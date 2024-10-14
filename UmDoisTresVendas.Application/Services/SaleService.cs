using AutoMapper;
using Serilog;
using UmDoisTresVendas.Application.DTOs;
using UmDoisTresVendas.Application.Interfaces;
using UmDoisTresVendas.Application.Validations;
using UmDoisTresVendas.Domain.Entities;
using UmDoisTresVendas.Domain.Entities.Enums;

namespace UmDoisTresVendas.Application.Services;

public class SaleService : ISaleService
{
    private readonly IBaseRepository<Sale> _saleRepository;
    private readonly SaleDtoValidator _saleDtoValidator;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    
    public SaleService(IBaseRepository<Sale> saleRepository, 
        SaleDtoValidator saleDtoValidator, 
        ILogger logger, 
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _saleDtoValidator = saleDtoValidator;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ApiResponseDto<GetSaleDto>> GetSaleByIdentificationAsync(Guid saleId)
    {
        try
        {
            var sale = await _saleRepository.GetByIdAsync(saleId);
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
}
