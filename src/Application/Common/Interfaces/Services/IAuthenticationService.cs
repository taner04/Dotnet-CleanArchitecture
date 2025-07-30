using Application.Dtos.User;
using Application.Response;

namespace Application.Common.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<Result<UserDto>> LoginAsync(UserLoginDto user);
        Task<Result<bool>> RegisterAsync(UserRegisterDto user);
    }
}
