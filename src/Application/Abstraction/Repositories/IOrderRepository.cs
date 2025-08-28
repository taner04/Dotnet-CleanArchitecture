using Domain.Entities.Orders;

namespace Application.Abstraction.Repositories;

public interface IOrderRepository : IRepository<Order, OrderId>
{
    Task<List<Order>> OrdersByUserAsync(UserId userId);
}