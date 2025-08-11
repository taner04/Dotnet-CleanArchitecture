using Application.Dtos.Jwt;
using Application.Dtos.User;
using Application.Response;

namespace Application.Common.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<ResultT<AuthResponse>> LoginAsync(LoginRequest user);
        Task<Result> RegisterAsync(RegisterRequest user);
        Task<ResultT<RefreshTokenResponse>> RefreshTokenAsync(UserId userId);
    }
}
