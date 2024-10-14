using System.Runtime.InteropServices.JavaScript;
using UmDoisTresVendas.Domain.Entities.Enums;

namespace UmDoisTresVendas.Domain.Entities;

public class Sale
{
    public Guid Id { get; private set; }
    public string SaleIdentification { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public List<SaleItem> Items { get; private set; } = new List<SaleItem>();
    public string CustomerId { get; private set; }
    public string CustomerName { get; private set; }
    public string BranchId { get; private set; }
    public string BranchName { get; private set; }
    public SaleStatusEnum Status { get; private set; }
    public decimal TotalPrice { get; private set; }

    public Sale() { }
    
    public Sale(string customerId, string customerName, string branchId, string branchName, List<SaleItem> items)
    {
        Id = Guid.NewGuid();
        SaleIdentification = GenerateSaleIdentification(customerId, branchId, DateTime.Now);
        CreatedAt = DateTime.Now;
        UpdatedAt = null;
        CustomerId = customerId;
        CustomerName = customerName;
        BranchId = branchId;
        BranchName = branchName;
        Status = SaleStatusEnum.Created;
        
        foreach (var item in items)
        {
            AddItem(item);
        }
    }
    
    public void AddItem(SaleItem item)
    {
        if (Status == SaleStatusEnum.Cancelled)
            throw new InvalidOperationException("Cannot add items to a canceled sale.");

        Items.Add(item);
        UpdateTotalPrice();
    }
    
    public void RemoveItem(SaleItem item)
    {
        if (Status == SaleStatusEnum.Cancelled)
            throw new InvalidOperationException("Cannot remove items from a canceled sale.");

        Items.Remove(item);
        UpdateTotalPrice();
    }

    public void UpdateStatus(SaleStatusEnum status)
    {
        Status = status;
    }
    
    private void UpdateTotalPrice()
    {
        TotalPrice = Items.Sum(item => item.TotalPrice());
    }
    
    public decimal CalculateTotal()
    {
        return TotalPrice;
    }
    
    public void UpdateFromDto(SaleStatusEnum status, List<SaleItem> items)
    {
        UpdateStatus(status);
        UpdatedAt = DateTime.Now;
        
        foreach (var item in items)
        {
            var newItem = new SaleItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice, item.Discount);
            AddItem(newItem); 
        }
    }
    
    private string GenerateSaleIdentification(string customerId, string branchId, DateTime date)
    {
        return $"{customerId}-{branchId}-{Guid.NewGuid().ToString("N")[..6]}-{date:ddMMyyyy}";
    }
}
