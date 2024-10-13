namespace UmDoisTresVendas.Domain.Entities.Enums;

public class SaleItem
{
    public Guid Id { get; }
    public string ProductId { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }
    public decimal Discount { get; }

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