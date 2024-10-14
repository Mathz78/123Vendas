using FluentValidation;
using UmDoisTresVendas.Application.DTOs;

namespace UmDoisTresVendas.Application.Validations;

public class SaleItemDtoValidator : AbstractValidator<SaleItemDto>
{
    public SaleItemDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId cannot be null or empty.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("UnitPrice must be greater than zero.");

        RuleFor(x => x.Discount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount must be zero or greater.");
    }
}