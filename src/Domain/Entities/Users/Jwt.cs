using Domain.ValueObjects;

namespace Domain.Entities.Users;

/// <summary>
/// Represents a JWT (JSON Web Token) entity containing refresh token and its expiration.
/// </summary>
public sealed class Jwt
{
    /// <summary>
    /// The expiration time in minutes for the access token.
    /// </summary>
    public const int AccessTokenExpirationMinutes = 60; // 1 hour

    /// <summary>
    /// The expiration time in days for the refresh token.
    /// </summary>
    public const int RefreshTokenExpirationDays = 30; // 30 days

    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private Jwt()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Jwt"/> class with the specified refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token value object.</param>
    public Jwt(JwtToken refreshToken)
    {
        var dateToday = DateTime.UtcNow;

        RefreshToken = refreshToken;
        RefreshTokenExpiration = dateToday.AddDays(RefreshTokenExpirationDays);
    }

    /// <summary>
    /// Gets a value indicating whether the refresh token is expired.
    /// </summary>
    public bool IsRefreshTokenExpired => DateTime.UtcNow >= RefreshTokenExpiration.Value;

    /// <summary>
    /// Gets the refresh token.
    /// </summary>
    public JwtToken RefreshToken { get; private set; }

    /// <summary>
    /// Gets the expiration date of the refresh token.
    /// </summary>
    public JwtTokenExpiration RefreshTokenExpiration { get; private set; }
}