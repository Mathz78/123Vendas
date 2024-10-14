namespace UmDoisTresVendas.Domain.Responses;

public class CreateSaleResponse
{
    public string SaleIdentification { get; set; }

    public CreateSaleResponse(string saleIdentification)
    {
        SaleIdentification = saleIdentification;
    }
}
