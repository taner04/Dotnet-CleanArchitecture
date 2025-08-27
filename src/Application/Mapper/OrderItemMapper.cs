using Application.Dtos.OrderItem;
using Domain.Entities.Orders;

namespace Application.Mapper;

public static class OrderItemMapper
{
    public static OrderItemDto ToOrderItemOrderDto(this OrderItem orderItem)
    {
        return new OrderItemDto(
            orderItem.Quantity,
            orderItem.UnitPrice.Value,
            orderItem.Product.ToProductSnapshotDto()
        );
    }
}