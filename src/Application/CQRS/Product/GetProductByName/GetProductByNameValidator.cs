namespace Application.CQRS.Product.GetProductByName;

public sealed class GetProductByNameValidator : AbstractValidator<GetProductByNameQuery>
{
    public GetProductByNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name cannot be empty.")
            .MaximumLength(100)
            .WithMessage("Product name cannot exceed 100 characters.");
    }
}