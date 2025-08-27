using Application.Dtos.Order;
using Domain.Entities.Orders;

namespace Application.Mapper;

public static class OrderMapper
{
    public static OrderDto ToOrderDto(this Order order)
    {
        return new OrderDto(
            order.Id.Value,
            order.TotalPrice,
            [.. order.OrderItems.Select(oi => oi.ToOrderItemOrderDto())]
        );
    }
}