using UmDoisTresVendas.Application.DTOs;
using UmDoisTresVendas.Domain.Entities;

namespace UmDoisTresVendas.Application.Interfaces;

public interface ISaleService
{
    /// <summary>
    /// Get sale details
    /// </summary>
    /// <param name="saleId">GUID Sale ID</param>
    /// <returns>Returns a DTO containing sale details</returns>
    public Task<ApiResponseDto<GetSaleDto>> GetSaleByIdAsync(Guid saleId);
    
    /// <summary>
    /// Creates a new sales
    /// </summary>
    /// <param name="createSaleDto">Sale DTO</param>
    /// <returns>Returns a DTO containing the generated IDs</returns>
    public Task<ApiResponseDto<CreateSaleResponseDto>> CreateSaleAsync(CreateSaleDto createSaleDto);
    
    /// <summary>
    /// Updates a sale
    /// </summary>
    /// <param name="saleId">GUID Sale ID</param>
    /// <param name="updateSaleDto">DTO with updated items</param>
    /// <returns>Returns a DTO containing the updated items</returns>
    public Task<ApiResponseDto<UpdateSaleDto>> UpdateSaleAsync(Guid saleId, UpdateSaleDto updateSaleDto);
    
    /// <summary>
    /// Cancel a sale
    /// </summary>
    /// <param name="saleId">GUID Sale ID</param>
    /// <returns>Returns a DTO containing the canceled Sale ID</returns>
    public Task<ApiResponseDto<CancelSaleDto>> CancelSaleAsync(Guid saleId);
}