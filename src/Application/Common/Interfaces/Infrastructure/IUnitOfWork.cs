using Application.Common.Interfaces.Infrastructure.Repositories;

namespace Application.Common.Interfaces.Infrastructure;

/// <summary>
/// Represents a unit of work that encapsulates multiple repository operations
/// and coordinates the saving of changes as a single transaction.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets the user repository.
    /// </summary>
    IUserRepository UserRepository { get; }

    /// <summary>
    /// Gets the product repository.
    /// </summary>
    IProductRepository ProductRepository { get; }

    /// <summary>
    /// Gets the order repository.
    /// </summary>
    IOrderRepository OrderRepository { get; }

    /// <summary>
    /// Gets the cart repository.
    /// </summary>
    ICartRepository CartRepository { get; }

    /// <summary>
    /// Saves all changes made in the unit of work to the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}