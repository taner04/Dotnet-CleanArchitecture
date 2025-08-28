using Application.Validator;

namespace Application.CQRS.Product.GetProductDetails;

public sealed class GetProductDetailsQueryValidator : AbstractValidator<GetProductDetailsQuery>
{
    public GetProductDetailsQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .IsId()
            .WithMessage("ID needs to be a valid Guid");
    }
}