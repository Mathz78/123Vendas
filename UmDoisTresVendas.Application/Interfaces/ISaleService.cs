using UmDoisTresVendas.Application.DTOs;
using UmDoisTresVendas.Domain.Entities;

namespace UmDoisTresVendas.Application.Interfaces;

public interface ISaleService
{
    public Task<ApiResponseDto<GetSaleDto>> GetSaleByIdentificationAsync(Guid saleId);
    public Task<ApiResponseDto<CreateSaleResponseDto>> CreateSaleAsync(CreateSaleDto createSaleDto);
    public Task<ApiResponseDto<CancelSaleDto>> CancelSaleAsync(Guid saleId);
}