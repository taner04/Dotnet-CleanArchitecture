using Domain.Entities.Orders;
using OrderId = Domain.ValueObjects.Identifiers.OrderId;
using UserId = Domain.ValueObjects.Identifiers.UserId;

namespace Application.Abstraction.Repositories;

/// <summary>
/// Represents a repository for managing <see cref="Order"/> entities.
/// </summary>
public interface IOrderRepository : IRepository<Order, OrderId>
{
    /// <summary>
    /// Asynchronously retrieves all orders associated with the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose orders are to be retrieved.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of <see cref="Order"/> objects for the specified user.
    /// </returns>
    Task<List<Order>> OrdersByUserAsync(UserId userId);
}