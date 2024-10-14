using UmDoisTresVendas.Domain.Entities.Enums;

namespace UmDoisTresVendas.Application.DTOs;

public class GetSaleDto
{
    public Guid Id { get; set; }
    public string SaleIdentification { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string BranchId { get; set; }
    public string BranchName { get; set; }
    public List<GetSaleItemDto> Items { get; set; }
    public SaleStatusEnum Status { get; set; }
    public decimal TotalPrice { get; set; }
}
