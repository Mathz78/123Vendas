using System.Runtime.InteropServices.ComTypes;
using UmDoisTresVendas.Domain.Entities.Enums;

namespace UmDoisTresVendas.Domain.Entities;

public class Sale
{
    public Guid Id { get; private set; }
    public string SaleIdentification { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public List<SaleItem> Items { get; private set; }
    public string CustomerId { get; private set; }
    public string BranchId { get; private set; }
    public SaleStatusEnum Status { get; private set; }

    public Sale() { }
    
    public Sale(string customerId, string branchId, List<SaleItem> items)
    {
        Id = Guid.NewGuid();
        SaleIdentification = GenerateSaleIdentification(customerId, branchId, DateTime.Now);
        CreatedAt = DateTime.Now;
        Items = items;
        CustomerId = customerId;
        BranchId = branchId;
        Status = SaleStatusEnum.Created;
    }

    public void AddItem(SaleItem item)
    {
        if (Status == SaleStatusEnum.Cancelled)
            throw new InvalidOperationException("Cannot add items to a canceled sale.");

        Items.Add(item);
    }

    public decimal CalculateTotal()
    {
        return Items.Sum(item => item.TotalPrice());
    }
    
    private string GenerateSaleIdentification(string customerId, string branchId, DateTime date)
    {
        return $"{customerId}-{branchId}-{Guid.NewGuid().ToString("N")[..6]}-{date:ddMMyyyy}";
    }
}
