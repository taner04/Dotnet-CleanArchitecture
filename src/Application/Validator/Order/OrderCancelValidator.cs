using Application.Dtos.Order;
using FluentValidation;

namespace Application.Validator.Order
{
    public sealed class OrderCancelValidator : AbstractValidator<OrderCancelDto>
    {
        public OrderCancelValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.");
            
            RuleFor(x => x.OrderId)
                .NotEmpty()
                .WithMessage("OrderId is required.");
        }
    }
}
