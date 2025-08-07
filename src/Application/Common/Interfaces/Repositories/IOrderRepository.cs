using Domain.Entities.Orders;

namespace Application.Common.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order, OrderId>
    {
        Task<List<Order>> GetAllByUserIdAsync(UserId userId);
    }
}
