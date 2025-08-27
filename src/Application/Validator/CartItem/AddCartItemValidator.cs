using Application.Dtos.Cart;
using Application.Dtos.CartItem;
using Application.Extensions;
using FluentValidation;

namespace Application.Validator.CartItem;

public sealed class AddCartItemValidator : AbstractValidator<AddCartItemDto>
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