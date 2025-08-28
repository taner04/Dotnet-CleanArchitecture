using Application.Validator;

namespace Application.CQRS.Cart.GetCart;

public sealed class GetCartByUserValidator : AbstractValidator<GetCartByUserQuery>
{
    public GetCartByUserValidator()
    {
        RuleFor(x => x.UserId)
            .IsId()
            .WithMessage("User Id needs to be a valid Guid");
    }
}