namespace Application.Common.Abstraction.Infrastructure;

/// <summary>
///     Defines methods for generating and validating access and refresh tokens for a applicationUser.
/// </summary>
/// <typeparam name="TUser">The type representing the applicationUser.</typeparam>
public interface ITokenService<in TUser>
{
    /// <summary>
    ///     Generates an access token for the specified applicationUser.
    /// </summary>
    /// <param name="user">The applicationUser for whom to generate the access token.</param>
    /// <returns>The generated access token as a string.</returns>
    string GenerateAccessToken(TUser user);

    /// <summary>
    ///     Generates a refresh token for the specified applicationUser.
    /// </summary>
    /// <param name="user">The applicationUser for whom to generate the refresh token.</param>
    /// <returns>The generated refresh token as a string.</returns>
    string GenerateRefreshToken(TUser user);

    /// <summary>
    ///     Validates the specified refresh token.
    /// </summary>
    /// <param name="token">The refresh token to validate.</param>
    /// <returns><c>true</c> if the token is valid; otherwise, <c>false</c>.</returns>
    bool IsRefreshTokenValid(string token);
}