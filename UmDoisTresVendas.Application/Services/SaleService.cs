using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using UmDoisTresVendas.Application.DTOs;
using UmDoisTresVendas.Application.Interfaces;
using UmDoisTresVendas.Application.Validations;
using UmDoisTresVendas.Domain.Entities;
using UmDoisTresVendas.Domain.Entities.Enums;

namespace UmDoisTresVendas.Application.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly SaleDtoValidator _saleDtoValidator;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    
    public SaleService(ISaleRepository saleRepository, 
        SaleDtoValidator saleDtoValidator, 
        ILogger logger, 
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _saleDtoValidator = saleDtoValidator;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ApiResponseDto<GetSaleDto>> GetSaleByIdAsync(Guid saleId)
    {
        try
        {
            var sale = await _saleRepository.Query()
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == saleId);
            if (sale == null)
            {
                _logger.Information("No sale was found for the given Id: {saleId}", saleId);
                return new ApiResponseDto<GetSaleDto>(false, 
                    new List<string> { "No sale was found for the given Id." });
            }
            
            var saleDto = _mapper.Map<GetSaleDto>(sale);
            _logger.Information("The data for the sale: {saleId} was successfully retrieved.", saleId);
            
            return new ApiResponseDto<GetSaleDto>(saleDto);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error occurred while trying to retrieve data for a sale.");
            return new ApiResponseDto<GetSaleDto>(false,
                ["Error occurred while trying to insert a new sale."]); 
        }
    }
    
    public async Task<ApiResponseDto<CreateSaleResponseDto>> CreateSaleAsync(CreateSaleDto createSaleDto)
    {
        try
        {
            var validationResult = await _saleDtoValidator.ValidateAsync(createSaleDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                _logger.Information("Errors are being returned with the validation result.");
                return new ApiResponseDto<CreateSaleResponseDto>(false, errorMessages);
            }
        
            var items = new List<SaleItem>();
            foreach (var item in createSaleDto.Items)
            {
                items.Add(new SaleItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice, item.Discount));
            }

            var sale = new Sale(createSaleDto.CustomerId, createSaleDto.CustomerName, createSaleDto.BranchId, 
                createSaleDto.BranchName, items);
            await _saleRepository.AddAsync(sale);
            
            PublishSaleEvent(nameof(CreateSaleAsync), sale.Id, new { SaleTotal = sale.TotalPrice });
            
            _logger.Information("Sucessfully added new sale {saleIdentification}", sale.SaleIdentification);
            return new ApiResponseDto<CreateSaleResponseDto>(new CreateSaleResponseDto(sale.Id.ToString(), 
                sale.SaleIdentification));
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error occurred while trying to insert a new sale.");
            return new ApiResponseDto<CreateSaleResponseDto>(false,
                ["Error occurred while trying to insert a new sale."]); 
        }
    }

    public async Task<ApiResponseDto<UpdateSaleDto>> UpdateSaleAsync(Guid saleId, UpdateSaleDto updateSaleDto)
    {
        try
        {
            var sale = await _saleRepository.GetByIdAsync(saleId);
            if (sale == null)
            {
                _logger.Information("No sale was found for the given Id: {saleId}", saleId);
                return new ApiResponseDto<UpdateSaleDto>(false, 
                    new List<string> { "No sale was found for the given Id." });
            }
            
            sale.UpdateFromDto(updateSaleDto.SaleStatus, 
                _mapper.Map<List<SaleItem>>(updateSaleDto.Items));
            await _saleRepository.UpdateAsync(sale);
            
            PublishSaleEvent(nameof(UpdateSaleAsync), sale.Id, updateSaleDto);
            
            _logger.Information("Sucessfully updated sale {saleId}", saleId);
            return new ApiResponseDto<UpdateSaleDto>(new UpdateSaleDto(updateSaleDto.Items, sale.Status));
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error occurred while trying to update a sale.");
            return new ApiResponseDto<UpdateSaleDto>(false,
                ["Error occurred while trying to update a sale."]);
        }
    }
    
    public async Task<ApiResponseDto<CancelSaleDto>> CancelSaleAsync(Guid saleId)
    {
        try
        {
            var sale = await _saleRepository.GetByIdAsync(saleId);
            if (sale == null)
            {
                _logger.Information("No sale was found for the given Id: {saleId}", saleId);
                return new ApiResponseDto<CancelSaleDto>(false,
                    ["No sale was found for the given Id."]);
            }

            if (sale.Status == SaleStatusEnum.Cancelled)
            {
                _logger.Information("Sale Id: {saleId} is already canceled.", saleId);
                return new ApiResponseDto<CancelSaleDto>(false,
                    ["Sale is already canceled."]);
            }
            
            sale.UpdateStatus(SaleStatusEnum.Cancelled);
            await _saleRepository.UpdateAsync(sale);
            
            PublishSaleEvent(nameof(CancelSaleAsync), sale.Id);
            
            _logger.Information("Sucessfully canceled sale {saleId}", saleId);
            return new ApiResponseDto<CancelSaleDto>(new CancelSaleDto(sale.SaleIdentification));
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error occurred while trying to cancel a sale.");
            return new ApiResponseDto<CancelSaleDto>(false,
                ["Error while trying to cancel a sale."]);
        }
    }
    
    private void PublishSaleEvent(string eventType, Guid saleId, object additionalData = null)
    {
        var message = $"Event: {eventType} | SaleId: {saleId} | Date: {DateTime.Now}";

        if (additionalData != null)
        {
            message += $" | Additional Data: {JsonSerializer.Serialize(additionalData)}";
        }

        _logger.Information(message);
    }
}
