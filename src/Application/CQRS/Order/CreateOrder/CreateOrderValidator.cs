using Application.Validator;

namespace Application.CQRS.Order.CreateOrder;

public sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(order => order.Products)
            .NotEmpty()
            .WithMessage("Order items cannot be empty.")
            .Must(items => items.Count > 0)
            .WithMessage("At least one order item is required.");
    }
}