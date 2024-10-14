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

    public Sale(string saleIdentification, DateTime createdAt, string customerId, string branchId, SaleStatusEnum status)
    {
        Id = Guid.NewGuid();
        SaleIdentification = saleIdentification;
        CreatedAt = createdAt;
        Items = new List<SaleItem>();
        CustomerId = customerId;
        BranchId = branchId;
        Status = status;
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
}
