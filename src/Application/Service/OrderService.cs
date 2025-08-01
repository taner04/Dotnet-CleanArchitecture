using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Dtos.Order;
using Application.Mapper;
using Application.Response;
using SharedKernel.Attributes;
using SharedKernel.Enums;

namespace Application.Service
{
    [ServiceInjection(typeof(IOrderService), ScopeType.AddTransient)]
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<List<OrderByIdDto>>> GetOrdersByUser(UserId userId)
        {
            var orders = await _orderRepository.GetAllByUserIdAsync(userId) ?? [];
            return Result<List<OrderByIdDto>>.Success(orders.Select(OrderMapper.ToOrderByIdDto).ToList());
        }

        public async Task<Result<bool>> CancelOrderAsync(OrderCancelDto orderCancel)
        {
            var order = await _orderRepository.GetByIdAsync(orderCancel.OrderId);

            if (order is null)
            {
                return Result<bool>.Failure(ErrorFactory.NotFound("Order not found."));
            }

            if (order.UserId != orderCancel.UserId)
            {
                return Result<bool>.Failure(ErrorFactory.Forbidden("You do not have permission to cancel this order."));
            }

            if (order.OrderStatus != OrderStatus.Pending)
            {
                return Result<bool>.Failure(ErrorFactory.BadRequest("Only pending orders can be canceled."));
            }

            _orderRepository.Delete(order);

            await _orderRepository.DbContext.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
