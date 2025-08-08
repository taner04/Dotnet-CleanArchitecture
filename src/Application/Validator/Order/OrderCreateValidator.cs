using Application.Dtos.Order;
using FluentValidation;

namespace Application.Validator.Order
{
    public sealed class OrderCreateValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateValidator() 
        {
            RuleFor(order => order.UserId)
                .NotEmpty()
                .WithMessage("User ID cannot be empty.");

            RuleFor(order => order.Products)
                .NotEmpty()
                .WithMessage("Order items cannot be empty.")
                .Must(items => items.Count > 0)
                .WithMessage("At least one order item is required.");
        }
    }
}
