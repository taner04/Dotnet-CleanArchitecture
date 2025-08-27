using Application.Extensions;

namespace Application.CQRS.Order.GetOrders;

public sealed class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersQueryValidator()
    {
        RuleFor(x => x.UserId).IsId()
            .WithMessage("ID needs to be a valid Guid");
    }
}