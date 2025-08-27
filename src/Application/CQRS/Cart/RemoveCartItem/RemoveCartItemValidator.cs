using Application.Extensions;

namespace Application.CQRS.Cart.RemoveCartItem;

public sealed class RemoveCartItemValidator : AbstractValidator<RemoveCartItemCommand>
{
    public RemoveCartItemValidator()
    {
        RuleFor(x => x.CartItemId)
            .IsId()
            .WithMessage("Cart item id is invalid");

        RuleFor(x => x.UserId)
            .IsId()
            .WithMessage("User id is invalid");
    }
}