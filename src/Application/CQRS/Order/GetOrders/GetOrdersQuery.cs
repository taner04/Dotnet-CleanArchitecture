using Application.Dtos.Order;

namespace Application.CQRS.Order.GetOrders;

public readonly record struct GetOrdersQuery(Guid UserId) : IQuery<ResultT<List<OrderDto>>>;