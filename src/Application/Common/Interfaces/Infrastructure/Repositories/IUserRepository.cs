using Domain.Entities.Users;

namespace Application.Common.Interfaces.Infrastructure.Repositories;

/// <summary>
/// Repository interface for <see cref="User"/> entities.
/// </summary>
public interface IUserRepository : IRepository<User, UserId>
{
    /// <summary>
    /// Retrieves a user by their email address asynchronously.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    Task<User?> GetByEmailAsync(string email);
}