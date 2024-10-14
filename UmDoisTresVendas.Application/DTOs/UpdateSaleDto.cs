using UmDoisTresVendas.Domain.Entities.Enums;

namespace UmDoisTresVendas.Application.DTOs;

public class UpdateSaleDto
{
    public List<SaleItemDto> Items { get; set; }
    public SaleStatusEnum SaleStatus { get; set; }

    public UpdateSaleDto(List<SaleItemDto> items, SaleStatusEnum saleStatus)
    {
        Items = items;
        SaleStatus = saleStatus;
    }
}
