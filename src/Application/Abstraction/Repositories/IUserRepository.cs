using Domain.Entities.Users;
using Domain.ValueObjects;
using UserId = Domain.ValueObjects.Identifiers.UserId;

namespace Application.Abstraction.Repositories;

/// <summary>
/// Defines a contract for a repository that manages <see cref="User"/> entities.
/// </summary>
public interface IUserRepository : IRepository<User, UserId>
{
    /// <summary>
    /// Asynchronously retrieves a <see cref="User"/> entity by its email address.
    /// </summary>
    /// <param name="email">The email address of the user to retrieve.</param>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation, 
    /// with a result of the <see cref="User"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<User?> GetByEmailAsync(Email email);
}