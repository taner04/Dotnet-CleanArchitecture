using Application.Dtos.Product;

namespace Application.Dtos.OrderItem
{
    public readonly record struct OrderItemByOrderDto(int Quantity, decimal Amount, ProductByOrderDto Products);
}
