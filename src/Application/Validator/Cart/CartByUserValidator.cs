using Application.Dtos.Cart;
using Application.Extensions;
using FluentValidation;

namespace Application.Validator.Cart;

public sealed class CartByUserValidator : AbstractValidator<CartByUserDto>
{
    public CartByUserValidator()
    {
        RuleFor(x => x.UserId)
            .IsId()
            .WithMessage("User Id needs to be a valid Guid");
    }
}