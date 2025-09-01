using Application.Abstraction.Repositories;

namespace Application.Abstraction.Utils;

/// <summary>
/// Represents a unit of work that encapsulates access to multiple repositories and coordinates the saving of changes.
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
    /// Persists all changes made in the unit of work to the underlying data store asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The number of state entries written to the data store.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}