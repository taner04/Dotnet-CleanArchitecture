using Application.Dtos.Product;
using Application.Extensions;
using FluentValidation;

namespace Application.Validator.Product
{
    public sealed class ProductDetailsByIdValidator : AbstractValidator<ProductDetailsByIdDto>
    {
        public ProductDetailsByIdValidator()
        {
            RuleFor(x => x.ProductId).IsId()
                                     .WithMessage("ID needs to be a valid Guid");
        }
    }
}
