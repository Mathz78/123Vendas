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
    
    public SaleService(IBaseRepository<Sale> saleRepository, SaleDtoValidator saleDtoValidator)
    {
        _saleRepository = saleRepository;
        _saleDtoValidator = saleDtoValidator;
    }

    public async Task<ApiResponse<string>> CreateSaleAsync(CreateSaleDto createSaleDto)
    {
        var validationResult = await _saleDtoValidator.ValidateAsync(createSaleDto);
        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return new ApiResponse<string>(errorMessages);
        }
        
        var items = new List<SaleItem>();
        foreach (var item in createSaleDto.Items)
        {
            items.Add(new SaleItem(item.ProductId, item.Quantity, item.UnitPrice, item.Discount));
        }
        
        var sale = new Sale(createSaleDto.CustomerId, createSaleDto.BranchId, items);
        await _saleRepository.AddAsync(sale);
        
        return new ApiResponse<string>(sale.SaleIdentification);
    }

    public Task<Sale> GetSaleByIdAsync(string saleId)
    {
        throw new NotImplementedException();
    }
}