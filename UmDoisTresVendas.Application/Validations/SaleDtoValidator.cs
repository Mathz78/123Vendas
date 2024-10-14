using FluentValidation;
using UmDoisTresVendas.Application.DTOs;

namespace UmDoisTresVendas.Application.Validations;

public class SaleDtoValidator : AbstractValidator<CreateSaleDto>
{
    public SaleDtoValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage($"The field {nameof(CreateSaleDto.CustomerId)} cannot be null or empty.");

        RuleFor(x => x.CustomerName)
            .NotEmpty()
            .WithMessage($"The field {nameof(CreateSaleDto.CustomerName)} cannot be null or empty.");
        
        RuleFor(x => x.BranchId)
            .NotEmpty()
            .WithMessage($"The field {nameof(CreateSaleDto.BranchId)} cannot be null or empty.");
        
        RuleFor(x => x.BranchName)
            .NotEmpty()
            .WithMessage($"The field {nameof(CreateSaleDto.BranchName)} cannot be null or empty.");
        
        RuleFor(x => x.Items)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Items list cannot be null.")
            .Must(items => items.Count > 0).WithMessage("Items list must contain at least one item.")
            .ForEach(item => item.SetValidator(new SaleItemDtoValidator()));
    }
}