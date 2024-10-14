namespace UmDoisTresVendas.Domain.Entities.Enums;

public class SaleItem
{
    public Guid Id { get; private set; }
    public string ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }

    public SaleItem(string productId, int quantity, decimal unitPrice, decimal discount)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
    }
    
    public decimal TotalPrice()
    { 
        return (UnitPrice * Quantity) - Discount;   
    }
}
