using UmDoisTresVendas.Domain.Entities.Enums;

namespace UmDoisTresVendas.Application.DTOs;

public class UpdateSaleDto
{
    public string SaleIdentification { get; set; }
    public List<SaleItemDto> Items { get; set; }
    public SaleStatusEnum SaleStatus { get; set; }
}
