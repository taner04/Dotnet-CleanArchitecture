using Application.Dtos.Order;
using SharedKernel.Response;
using SharedKernel.Response.Results;

namespace Application.Common.Interfaces.Services;

/// <summary>
/// Service interface for orders.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Retrieves a list of orders for a specific user.
    /// </summary>
    /// <param name="orderByUser">DTO containing user information and filter criteria.</param>
    /// <returns>A result containing a list of order DTOs.</returns>
    Task<ResultT<List<OrderDto>>> GetOrdersByUserAsync(OrderByUserDto orderByUser);

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="orderCreate">DTO containing order creation details.</param>
    /// <returns>A result indicating success or failure.</returns>
    Task<Result> CreateOrderAsync(CreateOrderRequest orderCreate);

    /// <summary>
    /// Cancels an existing order.
    /// </summary>
    /// <param name="orderCancelDto">DTO containing cancellation details.</param>
    /// <returns>A result indicating success or failure.</returns>
    Task<Result> CancelOrderAsync(CancelOrderRequest orderCancelDto);
}