using AutoMapper;
using UmDoisTresVendas.Application.DTOs;
using UmDoisTresVendas.Domain.Entities;
using UmDoisTresVendas.Domain.Entities.Enums;

namespace UmDoisTresVendas.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Sale, GetSaleDto>();
        CreateMap<SaleItem, GetSaleItemDto>();
        CreateMap<SaleItemDto, SaleItem>()
            .ConstructUsing(src => new SaleItem(src.ProductId, 
                src.ProductName, src.Quantity, src.UnitPrice, src.Discount));
    }
}