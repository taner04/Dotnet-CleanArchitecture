using Application.Dtos.OrderItem;
using Domain.Entities.Orders;

namespace Application.Mapper
{
    public static class OrderItemMapper
    {
        public static OrderItemOrderDto ToOrderItemOrderDto(this OrderItem orderItem)
        {
            return new OrderItemOrderDto(
                orderItem.Quantity,
                orderItem.UnitPrice.Value,
                orderItem.Product.ToProductOrderDto()
            );
        }
    }
}
