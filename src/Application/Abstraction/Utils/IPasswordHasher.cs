namespace Application.Abstraction.Utils;

/// <summary>
/// Provides methods for hashing and verifying passwords.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes the specified plain text password.
    /// </summary>
    /// <param name="password">The plain text password to hash.</param>
    /// <returns>The hashed representation of the password.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifies that a plain text password matches the specified hashed password.
    /// </summary>
    /// <param name="password">The plain text password to verify.</param>
    /// <param name="hashedPassword">The hashed password to compare against.</param>
    /// <returns><c>true</c> if the password matches the hashed password; otherwise, <c>false</c>.</returns>
    bool VerifyPassword(string password, string hashedPassword);
}