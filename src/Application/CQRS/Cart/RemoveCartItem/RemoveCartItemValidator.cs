using Application.Validator;

namespace Application.CQRS.Cart.RemoveCartItem;

public sealed class RemoveCartItemValidator : AbstractValidator<RemoveCartItemCommand>
{
    public RemoveCartItemValidator()
    {
        RuleFor(x => x.CartItemId)
            .IsId()
            .WithMessage("Cart item id is invalid");
    }
}