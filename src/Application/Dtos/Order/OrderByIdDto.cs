using Application.Dtos.OrderItem;

namespace Application.Dtos.Order
{
    public readonly record struct OrderByIdDto(int Id, decimal Amount, DateTime OrderDate, string PaymentMethod, string OrderStatus, IReadOnlyList<OrderItemByOrderDto> OrderItems);
}
