using Application.Dtos.Product;
using FluentValidation;

namespace Application.Validator.Product;

public sealed class ProductByNameValidator : AbstractValidator<ProductByNameDto>
{
    public ProductByNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name cannot be empty.")
            .MaximumLength(100)
            .WithMessage("Product name cannot exceed 100 characters.");
    }
}