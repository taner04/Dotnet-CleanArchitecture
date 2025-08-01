using Application.Dtos.OrderItem;

namespace Application.Mapper
{
    public static class OrderItemMapper
    {
        public static OrderItemByOrderDto ToOrderItemByOrderDto(OrderItem orderItem)
        {
            return new(
                orderItem.Quantity,
                orderItem.Amount,
                ProductMapper.ToProductByOrderDto(orderItem.Product)
            );
        }
    }
}
