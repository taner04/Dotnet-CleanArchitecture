using Application.Dtos.Order;
using Application.Extensions;
using FluentValidation;

namespace Application.Validator.Order
{
    public sealed class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator() 
        {
            RuleFor(order => order.UserId).IsId()
                                          .WithMessage("ID needs to be a valid Guid");

            RuleFor(order => order.Products)
                .NotEmpty()
                .WithMessage("Order items cannot be empty.")
                .Must(items => items.Count > 0)
                .WithMessage("At least one order item is required.");
        }
    }
}
