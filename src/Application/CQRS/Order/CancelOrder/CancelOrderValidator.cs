using Application.Extensions;

namespace Application.CQRS.Order.CancelOrder;

public sealed class CancelOrderValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderValidator()
    {
        RuleFor(x => x.OrderId).IsId()
            .WithMessage("ID needs to be a valid Guid");

        RuleFor(x => x.UserId).IsId()
            .WithMessage("ID needs to be a valid Guid");
    }
}