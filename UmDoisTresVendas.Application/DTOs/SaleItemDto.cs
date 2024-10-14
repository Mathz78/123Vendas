namespace UmDoisTresVendas.Application.DTOs;

public class SaleItemDto
{
    public required string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
}
