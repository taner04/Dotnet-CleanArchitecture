using Application.Dtos.Order;

namespace Application.CQRS.Order.GetOrders;

public readonly record struct GetOrdersQuery : IQuery<ResultT<List<OrderDto>>>;