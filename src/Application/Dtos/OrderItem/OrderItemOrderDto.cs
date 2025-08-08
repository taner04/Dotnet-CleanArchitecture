using Application.Dtos.Product;

namespace Application.Dtos.OrderItem
{
    public readonly record struct OrderItemOrderDto(decimal Quantity, decimal UnitPrice, decimal TotalPrice, ProductOrderDto Product);
}
