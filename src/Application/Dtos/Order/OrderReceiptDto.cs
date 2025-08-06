using Application.Dtos.Product;

namespace Application.Dtos.Order
{
    public readonly record struct OrderReceiptDto(decimal Amount, Guid TrackinNumber, List<ProductDto> Products);
}
