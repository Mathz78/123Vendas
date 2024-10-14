namespace UmDoisTresVendas.Domain.Entities.Enums;

public class SaleItem
{
    public Guid Id { get; private set; }
    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }

    public Guid SaleId { get; set; }
    
    public SaleItem(string productId, string productName, int quantity, decimal unitPrice, decimal discount)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
    }
    
    public decimal TotalPrice()
    { 
        return (UnitPrice * Quantity) - Discount;   
    }
}
