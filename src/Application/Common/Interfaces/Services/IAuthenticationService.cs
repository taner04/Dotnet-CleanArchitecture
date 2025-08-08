using Application.Dtos.Jwt;
using Application.Dtos.User;
using Application.Response;

namespace Application.Common.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<ResultT<UserDto>> LoginAsync(UserLoginDto user);
        Task<Result> RegisterAsync(UserRegisterDto user);
        Task<ResultT<JwtRefreshedTokenDto>> RefreshTokenAsync(UserId userId);
    }
}
