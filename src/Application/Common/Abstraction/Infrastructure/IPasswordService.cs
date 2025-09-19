namespace Application.Common.Abstraction.Infrastructure;

/// <summary>
/// Provides password hashing and verification services.
/// </summary>
public interface IPasswordService
{
    /// <summary>
    /// Hashes the specified plain text password.
    /// </summary>
    /// <param name="password">The plain text password to hash.</param>
    /// <returns>The hashed password as a string.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifies that the provided password matches the user's stored password.
    /// </summary>
    /// <param name="user">The user whose password is being verified.</param>
    /// <param name="providedPassword">The password to verify.</param>
    /// <returns><c>true</c> if the password is correct; otherwise, <c>false</c>.</returns>
    bool VerifyPassword(User user, string providedPassword);
}