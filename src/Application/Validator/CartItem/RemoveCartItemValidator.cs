using Application.Dtos.CartItem;
using Application.Extensions;
using FluentValidation;

namespace Application.Validator.CartItem;

public sealed class RemoveCartItemValidator : AbstractValidator<RemoveCartItemDto>
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