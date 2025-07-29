using Application.Dtos.User;
using Application.Response;

namespace Application.Common.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<ResultT<UserDto>> LoginAsync(UserLoginDto user);
        Task<ResultT<bool>> RegisterAsync(UserRegisterDto user);
    }
}
