namespace Application.Abstraction.Infrastructure;

/// <summary>
/// Provides access to the current user's information.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the unique identifier of the current user.
    /// </summary>
    /// <returns>The <see cref="UserId"/> of the current user, or null if not available.</returns>
    UserId? GetUserId();
}