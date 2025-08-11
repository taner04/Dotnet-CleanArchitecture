using Application.Dtos.Order;
using FluentValidation;

namespace Application.Validator.Order
{
    public sealed class OrderCancelValidator : AbstractValidator<OrderCancelDto>
    {
        public OrderCancelValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty()
                .WithMessage("Order ID is required.")
                .Must(x => Guid.TryParse(x.ToString(), out _))
                .WithMessage("Order ID must be a valid GUID.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required.")
                .Must(x => Guid.TryParse(x.ToString(), out _))
                .WithMessage("User ID must be a valid GUID.");
        }
    }
}
