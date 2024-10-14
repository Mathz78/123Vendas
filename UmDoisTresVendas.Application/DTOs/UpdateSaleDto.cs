using UmDoisTresVendas.Domain.Entities.Enums;

namespace UmDoisTresVendas.Application.DTOs;

public class UpdateSaleDto
{
    public required string SaleIdentification { get; set; }
    public required List<SaleItemDto> Items { get; set; }
    public SaleStatusEnum SaleStatus { get; set; }
}
