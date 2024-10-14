namespace UmDoisTresVendas.Application.DTOs;

public class CreateSaleDto
{
    public required string CustomerId { get; set; }
    public required string BranchId { get; set; }
    public required List<SaleItemDto> Items { get; set; }
}
