using System.Security.Claims;
using Domain.Entities.Users;

namespace Application.Abstraction.Utils;

/// <summary>
/// Provides methods for generating and validating JWT access and refresh tokens, and extracting claims.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a JWT access token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom to generate the access token.</param>
    /// <returns>A JWT access token as a string.</returns>
    string GenerateAccessToken(User user);

    /// <summary>
    /// Generates a JWT refresh token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom to generate the refresh token.</param>
    /// <returns>A JWT refresh token as a string.</returns>
    string GenerateRefreshToken(User user);

    /// <summary>
    /// Extracts all claims from the specified JWT token.
    /// </summary>
    /// <param name="token">The JWT token from which to extract claims.</param>
    /// <returns>A list of claims contained in the token.</returns>
    List<Claim> GetClaims(string token);

    /// <summary>
    /// Extracts a specific claim from the specified JWT token.
    /// </summary>
    /// <param name="token">The JWT token from which to extract the claim.</param>
    /// <param name="claimType">The type of claim to extract.</param>
    /// <returns>The claim matching the specified type, or <c>null</c> if not found.</returns>
    Claim GetClaim(string token, string claimType);

    /// <summary>
    /// Validates whether the specified refresh token is valid.
    /// </summary>
    /// <param name="token">The refresh token to validate.</param>
    /// <returns><c>true</c> if the refresh token is valid; otherwise, <c>false</c>.</returns>
    bool IsRefreshTokenValid(string token);
}