using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Dtos.Order;
using Application.Mapper;
using Application.Response;
using Application.Validator;
using Domain.DomainEvents.Order;
using Domain.Entities.Orders;
using Domain.ValueObjects;
using SharedKernel.Attributes;
using SharedKernel.Enums;

namespace Application.Service
{
    [ServiceInjection(typeof(IOrderService), ScopeType.AddTransient)]
    public sealed class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IValidatorFactory _validatorFactory;

        public OrderService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ResultT<List<OrderDto>>> GetOrdersByUserAsync(UserId userId)
        {
            var orders = await _unitOfWork.OrderRepository.OrdersByUserAsync(userId);
            return ResultT<List<OrderDto>>.Success([.. orders.Select(o => o.ToOrderDto())]);
        }

        //TODO: Save changes in a single transaction
        public async Task<Result> CreateOrderAsync(CreateOrderRequest orderCreate)
        {
            var validationResult = _validatorFactory.GetResult(orderCreate);
            if(!validationResult.IsValid)
            {
                return Result.Failure(
                    ErrorFactory.ValidationError(validationResult.ToDictionary())
                );
            }

            var userId = UserId.From(orderCreate.UserId);

            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
            {
                return Result.Failure(
                    ErrorFactory.NotFound($"User with ID {orderCreate.UserId} not found.")
                );
            }

            var order = new Order(OrderId.From(Guid.CreateVersion7()), userId);
            foreach (var product in orderCreate.Products)
            {
                var productId = ProductId.From(product.ProductId);
                var productEntity = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
                
                if (productEntity is null)
                {
                    return Result.Failure(
                        ErrorFactory.NotFound($"Product with ID {product.ProductId} not found.")
                    );
                }

                if (!productEntity.HasSufficientStock(product.Quantity))
                {
                    return Result.Failure(
                        ErrorFactory.Conflict($"Insufficient stock for product with ID {product.ProductId}.")
                    );
                }

                productEntity.UpdateQuantity(-product.Quantity);
                _unitOfWork.ProductRepository.Update(productEntity);

                order.AddOrderItem(
                    productId, 
                    product.Quantity, 
                    Money.From(productEntity.Price)
                );
            }

            order.AddDomainEvent(new OrderConfirmationDomainEvent(userId, order));
            _unitOfWork.OrderRepository.Add(order);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> CancelOrderAsync(CancelOrderRequest orderCancelDto)
        {
            var validationResult = _validatorFactory.GetResult(orderCancelDto);
            if (!validationResult.IsValid)
            {
                Result.Failure(
                    ErrorFactory.ValidationError(validationResult.ToDictionary())
                );
            }

            var order = await _unitOfWork.OrderRepository.GetByIdAsync(OrderId.From(orderCancelDto.OrderId));
            if (order is null)
            {
                return Result.Failure(
                    ErrorFactory.NotFound($"Order with ID {orderCancelDto.OrderId} not found.")
                );
            }

            if(order.UserId != UserId.From(orderCancelDto.UserId))
            {
                return Result.Failure(
                    ErrorFactory.Unauthorized("You cannot cancel an order that does not belong to you.")
                );
            }

            _unitOfWork.OrderRepository.Delete(order);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
