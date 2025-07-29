using Application.Common.Interfaces.Services;
using Application.Dtos.User;
using Application.Response;

namespace Infrastructure.Services
{
    public sealed class AuthenticationService : IAuthenticationService
    {


        public Task<ResultT<UserDto>> LoginAsync(UserLoginDto user)
        {
            throw new NotImplementedException();
        }

        public Task<ResultT<bool>> RegisterAsync(UserRegisterDto user)
        {
            throw new NotImplementedException();
        }
    }
}
