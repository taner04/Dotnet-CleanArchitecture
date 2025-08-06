using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Dtos.Order;
using Application.Mapper;
using Application.Response;
using Application.Validator;
using SharedKernel.Attributes;
using SharedKernel.Enums;

namespace Application.Service
{
    [ServiceInjection(typeof(IOrderService), ScopeType.AddTransient)]
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IValidatorFactory _validatorFactory;

        public OrderService(IOrderRepository orderRepository, IValidatorFactory validatorFactory)
        {
            _orderRepository = orderRepository;
            _validatorFactory = validatorFactory;
        }

        public async Task<Result<List<OrderByIdDto>>> GetOrdersByUser(UserId userId)
        {
            var orders = await _orderRepository.GetAllByUserIdAsync(userId) ?? [];
            return Result<List<OrderByIdDto>>.Success(orders.Select(OrderMapper.ToOrderByIdDto).ToList());
        }

        public async Task<Result<bool>> CancelOrderAsync(OrderCancelDto orderCancel)
        {
            var validationResult = _validatorFactory.GetResult(orderCancel);
            if (!validationResult.IsValid)
            {
                return Result<bool>.Failure(
                    ErrorFactory.ValidationError(validationResult.ToDictionary())
                );
            }

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

        public async Task<Result<bool>> CreateOrderAsync(OrderCreateDto order)
        {
            var validationResult = _validatorFactory.GetResult(order);
            if (!validationResult.IsValid)
            {
                return Result<bool>.Failure(
                    ErrorFactory.ValidationError(validationResult.ToDictionary())
                );
            }

            var newOrder = OrderMapper.ToOrder(order);

            _orderRepository.Add(newOrder);
            await _orderRepository.DbContext.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
