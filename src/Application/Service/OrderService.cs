using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Dtos.Order;
using Application.Mapper;
using Application.Response;
using Application.Validator;
using Domain.Entities.Orders;
using Domain.ValueObjects;
using SharedKernel.Attributes;
using SharedKernel.Enums;

namespace Application.Service
{
    [ServiceInjection(typeof(IOrderService), ScopeType.AddTransient)]
    public sealed class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IValidatorFactory _validatorFactory;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IValidatorFactory validatorFactory)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _validatorFactory = validatorFactory;
        }

        public async Task<ResultT<List<OrderDto>>> GetOrdersByUserAsync(UserId userId)
        {
            var orders = await _orderRepository.OrdersByUserAsync(userId);
            return ResultT<List<OrderDto>>.Success([.. orders.Select(o => o.ToOrderDto())]);
        }

        //TODO: Save changes in a single transaction
        public async Task<Result> CreateOrderAsync(OrderCreateDto orderCreate)
        {
            var validationResult = _validatorFactory.GetResult(orderCreate);
            if(!validationResult.IsValid)
            {
                return Result.Failure(
                    ErrorFactory.ValidationError(validationResult.ToDictionary())
                );
            }

            var order = new Order(OrderId.From(Guid.CreateVersion7()), UserId.From(orderCreate.UserId));
            foreach (var product in orderCreate.Products)
            {
                var productId = ProductId.From(product.ProductId);
                var productEntity = await _productRepository.GetByIdAsync(productId);
                
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
                _productRepository.Update(productEntity);

                order.AddOrderItem(
                    productId, 
                    product.Quantity, 
                    Money.From(productEntity.Price)
                );
            }

            _orderRepository.Add(order);

            await _orderRepository.DbContext.SaveChangesAsync();
            await _productRepository.DbContext.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> CancelOrderAsync(OrderCancelDto orderCancelDto)
        {
            var validationResult = _validatorFactory.GetResult(orderCancelDto);
            if (!validationResult.IsValid)
            {
                Result.Failure(
                    ErrorFactory.ValidationError(validationResult.ToDictionary())
                );
            }

            var order = await _orderRepository.GetByIdAsync(OrderId.From(orderCancelDto.OrderId));
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

            _orderRepository.Delete(order);
            await _orderRepository.DbContext.SaveChangesAsync();

            return Result.Success();
        }
    }
}
