using Application.Common.Interfaces;
using Application.Common.Interfaces.Infrastructure;
using Application.Common.Interfaces.Services;
using Application.DomainEvents.Order.Event;
using Application.Dtos.Order;
using Application.Extensions;
using Application.Mapper;
using Domain.Entities.Orders;
using SharedKernel.Attributes;
using SharedKernel.Enums;
using SharedKernel.Response;

namespace Application.Service;

[ServiceInjection(typeof(IOrderService), ScopeType.Transient)]
public sealed class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidatorFactory _validatorFactory;

    public OrderService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
    }

    public async Task<ResultT<List<OrderDto>>> GetOrdersByUserAsync(OrderByUserDto orderByUserDto)
    {
        var orders = await _unitOfWork.OrderRepository.OrdersByUserAsync(orderByUserDto.UserId);
        return ResultT<List<OrderDto>>.Success([.. orders.Select(o => o.ToOrderDto())]);
    }

    public async Task<Result> CreateOrderAsync(CreateOrderRequest orderCreate)
    {
        var validationResult = _validatorFactory.GetResult(orderCreate);
        if (!validationResult.IsValid)
            return Result.Failure(
                ErrorFactory.ValidationError(validationResult.ToDictionary())
            );

        var userId = orderCreate.UserId;

        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        if (user is null)
            return Result.Failure(
                ErrorFactory.NotFound($"User with ID {orderCreate.UserId} not found.")
            );

        var order = Order.TryCreate(Guid.CreateVersion7(), userId);
        foreach (var (productId, quantity) in orderCreate.Products)
        {
            var productEntity = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (productEntity is null)
                return Result.Failure(
                    ErrorFactory.NotFound($"Product with ID {productId} not found.")
                );

            var result = productEntity.TryReduceStock(quantity);
            if (!result.IsSuccess) return result;

            order.AddOrderItem(productId, quantity, productEntity.Price);
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
            Result.Failure(
                ErrorFactory.ValidationError(validationResult.ToDictionary())
            );

        var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderCancelDto.OrderId);
        if (order is null)
            return Result.Failure(
                ErrorFactory.NotFound($"Order with ID {orderCancelDto.OrderId} not found.")
            );

        if (order.UserId != orderCancelDto.UserId)
            return Result.Failure(
                ErrorFactory.Unauthorized("You cannot cancel an order that does not belong to you.")
            );

        if (order.Status != OrderStatus.Pending)
            return Result.Failure(
                ErrorFactory.Conflict("Only pending orders can be cancelled.")
            );

        order.UpdateStatus(OrderStatus.Cancelled);
        foreach (var orderItem in order.OrderItems)
        {
            var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(orderItem.ProductId);
            if (existingProduct == null) continue;

            var result = existingProduct.TryIncreaseStock(orderItem.Quantity);
            if (!result.IsSuccess) return result;
        }

        _unitOfWork.OrderRepository.Delete(order);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}