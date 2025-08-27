using Application.Dtos.OrderItem;

namespace Application.Dtos.Order;

public readonly record struct OrderDto(Guid Id, decimal TotalPrice, List<OrderItemDto> OrderItems);