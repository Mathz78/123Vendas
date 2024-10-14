using Serilog;
using UmDoisTresVendas.Application.DTOs;
using UmDoisTresVendas.Application.Interfaces;
using UmDoisTresVendas.Application.Validations;
using UmDoisTresVendas.Domain.Entities;
using UmDoisTresVendas.Domain.Entities.Enums;
using UmDoisTresVendas.Domain.Responses;

namespace UmDoisTresVendas.Application.Services;

public class SaleService : ISaleService
{
    private readonly IBaseRepository<Sale> _saleRepository;
    private readonly SaleDtoValidator _saleDtoValidator;
    private readonly ILogger _logger;
    
    public SaleService(IBaseRepository<Sale> saleRepository, SaleDtoValidator saleDtoValidator, ILogger logger)
    {
        _saleRepository = saleRepository;
        _saleDtoValidator = saleDtoValidator;
        _logger = logger;
    }

    public async Task<ApiResponse<CreateSaleResponse>> CreateSaleAsync(CreateSaleDto createSaleDto)
    {
        try
        {
            var validationResult = await _saleDtoValidator.ValidateAsync(createSaleDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new ApiResponse<CreateSaleResponse>(false, errorMessages);
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
            
            return new ApiResponse<CreateSaleResponse>(new CreateSaleResponse(sale.SaleIdentification));
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error occurred while trying to insert a new sale.");
            return new ApiResponse<CreateSaleResponse>(false,
                ["Error occurred while trying to insert a new sale."]); 
        }
    }

    public Task<Sale> GetSaleByIdAsync(string saleId)
    {
        throw new NotImplementedException();
    }
}
