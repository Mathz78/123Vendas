using UmDoisTresVendas.Application.DTOs;
using UmDoisTresVendas.Domain.Entities;
using UmDoisTresVendas.Domain.Responses;

namespace UmDoisTresVendas.Application.Interfaces;

public interface ISaleService
{
    public Task<ApiResponse<string>> CreateSaleAsync(CreateSaleDto createSaleDto);
    public Task<Sale> GetSaleByIdAsync(string saleId);
}