using Application.Dtos.Order;
using FluentValidation;

namespace Application.Validator.Order
{
    public sealed class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator() 
        {
            RuleFor(order => order.UserId)
                .NotEmpty()
                .WithMessage("User ID cannot be empty.")
                .Must(x => Guid.TryParse(x.ToString(), out _))
                .WithMessage("User ID must be a valid GUID.");

            RuleFor(order => order.Products)
                .NotEmpty()
                .WithMessage("Order items cannot be empty.")
                .Must(items => items.Count > 0)
                .WithMessage("At least one order item is required.");
        }
    }
}
