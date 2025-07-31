using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;

namespace Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> GetOrders(UserId userId)
        {
            var orders = await _orderRepository.GetAllByUserIdAsync(userId);

            return orders is null || orders.Count == 0 ? [] : orders;
        }
    }
}
