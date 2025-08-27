using Domain.Entities.Orders;

namespace Application.Common.Interfaces.Infrastructure.Repositories;

/// <summary>
/// Repository interface for managing <see cref="Order"/> entities.
/// </summary>
public interface IOrderRepository : IRepository<Order, OrderId>
{
    /// <summary>
    /// Retrieves a list of orders associated with the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of orders.</returns>
    Task<List<Order>> OrdersByUserAsync(UserId userId);
}