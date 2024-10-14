namespace UmDoisTresVendas.Application.DTOs;

public class CreateSaleDto
{
    public string? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? BranchId { get; set; }
    public string? BranchName { get; set; }
    public List<SaleItemDto>? Items { get; set; }
}
