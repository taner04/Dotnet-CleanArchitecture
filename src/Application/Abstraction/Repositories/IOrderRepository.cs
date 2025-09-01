using Domain.Entities.Orders;
using OrderId = Domain.ValueObjects.Identifiers.OrderId;
using UserId = Domain.ValueObjects.Identifiers.UserId;

namespace Application.Abstraction.Repositories;

public interface IOrderRepository : IRepository<Order, OrderId>
{
    Task<List<Order>> OrdersByUserAsync(UserId userId);
}