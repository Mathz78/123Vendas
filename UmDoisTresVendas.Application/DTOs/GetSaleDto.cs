using UmDoisTresVendas.Domain.Entities.Enums;

namespace UmDoisTresVendas.Application.DTOs;

public class GetSaleDto
{
    public Guid Id { get; set; }
    public required string SaleIdentification { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string CustomerId { get; set; }
    public required string BranchId { get; set; }
    public required List<GetSaleItemDto> Items { get; set; }
    public SaleStatusEnum Status { get; set; }
}
