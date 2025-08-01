namespace Application.Common.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order, OrderId>
    {
        Task<List<Order>> GetAllByUserIdAsync(UserId userId);
        Task<Order?> GetOrderToCancel(OrderId orderId, UserId userId);
    }
}
