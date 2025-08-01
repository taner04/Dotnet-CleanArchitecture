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
    }
}
