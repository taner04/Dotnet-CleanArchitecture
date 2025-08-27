using Application.Dtos.Order;
using Application.Extensions;
using FluentValidation;

namespace Application.Validator.Order;

public sealed class OrderByUserValidator : AbstractValidator<OrderByUserDto>
{
    public OrderByUserValidator()
    {
        RuleFor(x => x.UserId).IsId()
            .WithMessage("ID needs to be a valid Guid");
    }
}