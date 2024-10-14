using UmDoisTresVendas.Application.DTOs;
using UmDoisTresVendas.Domain.Entities;
using UmDoisTresVendas.Domain.Entities.Enums;

namespace UmDoisTresVendas.UnitTests;

public class BaseTest
{
    protected CreateSaleDto ValidCreateSaleDto = new CreateSaleDto
    {
        CustomerId = "customer-1",
        CustomerName = "Customer One",
        BranchId = "branch-1",
        BranchName = "Branch One",
        Items = new List<SaleItemDto>
        {
            new SaleItemDto { ProductId = "product-1", ProductName = "Product One", Quantity = 2, UnitPrice = 50, Discount = 5 }
        }
    };
}