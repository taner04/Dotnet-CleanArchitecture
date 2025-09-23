using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Abstraction.Persistence;

/// <summary>
///     Represents the abstraction for the Budget database context.
///     Provides access to Users, database operations, and save functionality.
/// </summary>
public interface IBudgetDbContext : IDisposable
{
    /// <summary>
    ///     Gets the set of users in the database.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    ///     Provides access to database-related operations.
    /// </summary>
    DatabaseFacade Database { get; }

    /// <summary>
    ///     Asynchronously saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}