using Application.Dtos.Order;
using SharedKernel.Extensions;

namespace Application.Mapper
{
    public static class OrderMapper
    {
        public static OrderByIdDto ToOrderByIdDto(Order order)
        {
            return new(
                order.Id.Value,
                order.Amount,
                order.OrderDate,
                order.PaymentMethod.GetDescription(),
                order.OrderStatus.GetDescription(),
                order.OrderItems.Select(OrderItemMapper.ToOrderItemByOrderDto).ToList()
            );
        }

        public static Order ToOrder(OrderCreateDto order)
        {
            return new Order(
                order.Products.Select(oi => oi.Price).Sum(),
                order.PaymentMethod,
                Guid.NewGuid().ToString()
            )
            {
                UserId = UserId.From(order.UserId),
                OrderItems = order.Products.Select(OrderItemMapper.ToOrderItem).ToList()
            };
        }
    }
}
