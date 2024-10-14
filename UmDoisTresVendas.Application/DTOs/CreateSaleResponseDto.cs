namespace UmDoisTresVendas.Application.DTOs;

public class CreateSaleResponseDto
{
    public string SaleId { get; set; }
    public string SaleIdentification { get; set; }

    public CreateSaleResponseDto(string saleId, string saleIdentification)
    {
        SaleId = saleId;
        SaleIdentification = saleIdentification;
    }
}
