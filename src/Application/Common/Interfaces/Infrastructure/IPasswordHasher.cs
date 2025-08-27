namespace Application.Common.Interfaces.Infrastructure;

/// <summary>
/// Provides password hashing and verification functionality.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes the specified plain text password.
    /// </summary>
    /// <param name="password">The plain text password to hash.</param>
    /// <returns>The hashed password as a string.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifies a plain text password against a hashed password.
    /// </summary>
    /// <param name="password">The plain text password to verify.</param>
    /// <param name="hashedPassword">The hashed password to compare against.</param>
    /// <returns>True if the password matches the hash; otherwise, false.</returns>
    bool VerifyPassword(string password, string hashedPassword);
}