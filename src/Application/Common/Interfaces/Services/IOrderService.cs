namespace Application.Common.Interfaces.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders(UserId userId);
    }
}
