using Application.Dtos.Order;
using Application.Extensions;
using FluentValidation;

namespace Application.Validator.Order;

public sealed class CancelOrderRequestValidator : AbstractValidator<CancelOrderRequest>
{
    public CancelOrderRequestValidator()
    {
        RuleFor(x => x.OrderId).IsId()
            .WithMessage("ID needs to be a valid Guid");

        RuleFor(x => x.UserId).IsId()
            .WithMessage("ID needs to be a valid Guid");
    }
}