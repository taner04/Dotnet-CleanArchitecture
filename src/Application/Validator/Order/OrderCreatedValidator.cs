using Application.Dtos.Order;
using FluentValidation;

namespace Application.Validator.Order
{
    public sealed class OrderCreatedValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreatedValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required.");

            RuleFor(x => x.Products)
                .NotEmpty()
                .WithMessage("Products cannot be empty.");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty()
                .WithMessage("Payment method is required.")
                .IsInEnum()
                .WithMessage("Invalid payment method.");

            RuleForEach(x => x.Products)
                .ChildRules(product =>
                {
                    product.RuleFor(p => p.ProductId)
                        .NotEmpty()
                        .WithMessage("Product ID is required.");

                    product.RuleFor(p => p.Quantity)
                        .GreaterThan(0)
                        .WithMessage("Quantity must be greater than zero.");
                
                    product.RuleFor(p => p.Price)
                        .GreaterThan(0)
                        .WithMessage("Price must be greater than zero.");
                });
        }
    }
}
