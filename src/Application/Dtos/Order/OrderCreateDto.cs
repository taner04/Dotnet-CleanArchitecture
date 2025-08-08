using Application.Dtos.Product;

namespace Application.Dtos.Order
{
    public readonly record struct OrderCreateDto(Guid UserId, List<ProductOrderCreateDto> Products);
}
