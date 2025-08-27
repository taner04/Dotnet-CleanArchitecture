using Domain.Entities.Users;
using System.Security.Claims;

namespace Application.Common.Interfaces.Infrastructure;

/// <summary>
/// Provides methods for generating and validating tokens for user authentication.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates an access token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom to generate the access token.</param>
    /// <returns>The generated access token as a string.</returns>
    string GenerateAccessToken(User user);

    /// <summary>
    /// Generates a refresh token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom to generate the refresh token.</param>
    /// <returns>The generated refresh token as a string.</returns>
    string GenerateRefreshToken(User user);

    /// <summary>
    /// Retrieves a specific claim from the provided token.
    /// </summary>
    /// <param name="token">The token to extract the claim from.</param>
    /// <param name="claimType">The type of claim to retrieve.</param>
    /// <returns>The claim if found; otherwise, null.</returns>
    Claim GetClaim(string token, string claimType);

    /// <summary>
    /// Validates whether the provided refresh token is valid.
    /// </summary>
    /// <param name="token">The refresh token to validate.</param>
    /// <returns>True if the token is valid; otherwise, false.</returns>
    bool IsRefreshTokenValid(string token);
}