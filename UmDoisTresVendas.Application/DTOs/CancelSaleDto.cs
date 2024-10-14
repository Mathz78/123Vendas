namespace UmDoisTresVendas.Application.DTOs;

public class CancelSaleDto
{
    public string? SaleIdentification { get; set; }

    public CancelSaleDto(string saleIdentification)
    {
        SaleIdentification = saleIdentification;
    }
}
