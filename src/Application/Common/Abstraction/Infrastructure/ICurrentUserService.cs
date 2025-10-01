using UserId = Domain.Entities.ApplicationUsers.UserId;

namespace Application.Common.Abstraction.Infrastructure;

/// <summary>
///     Provides access to the current applicationUser's information.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    ///     Gets the unique identifier of the current applicationUser.
    /// </summary>
    /// <returns>The <see cref="UserId" /> of the current applicationUser, or null if not available.</returns>
    UserId? GetUserId();
}