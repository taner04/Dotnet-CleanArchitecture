using Application.Dtos.Jwt;
using Application.Dtos.User;
using SharedKernel.Response;
using SharedKernel.Response.Results;

namespace Application.Common.Interfaces.Services;

/// <summary>
/// Service interface for authentication and refreshtoke .
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Authenticates a user and returns a JWT token upon successful login.
    /// </summary>
    /// <param name="user">The login request containing user credentials.</param>
    /// <returns>A result containing the authentication response.</returns>
    Task<ResultT<AuthResponse>> LoginAsync(LoginRequest user);

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="user">The registration request containing user details.</param>
    /// <returns>A result indicating the success or failure of the registration.</returns>
    Task<Result> RegisterAsync(RegisterRequest user);

    /// <summary>
    /// Refreshes the JWT token using a valid refresh token.
    /// </summary>
    /// <param name="refreshToken">The DTO containing the refresh token.</param>
    /// <returns>A result containing the new refresh token response.</returns>
    Task<ResultT<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenDto refreshToken);
}