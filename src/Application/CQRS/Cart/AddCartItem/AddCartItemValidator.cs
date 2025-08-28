using Application.Validator;

namespace Application.CQRS.Cart.AddCartItem;

public sealed class AddCartItemValidator : AbstractValidator<AddCartItemCommand>
{
    public AddCartItemValidator()
    {
        RuleFor(x => x.UserId)
            .IsId()
            .WithMessage("ID needs to be a valid Guid");

        RuleFor(x => x.ProductId)
            .IsId()
            .WithMessage("ID needs to be a valid Guid");
    }
}