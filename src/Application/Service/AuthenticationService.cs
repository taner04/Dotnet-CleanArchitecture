using Application.Common.Interfaces.Services;
using Application.Dtos.User;
using Application.Response;
using SharedKernel.Attributes;

namespace Application.Service
{
    [ServiceInjection(typeof(IAuthenticationService), SharedKernel.Enums.ScopeType.AddTransient)]
    public sealed class AuthenticationService : IAuthenticationService
    {
        public Task<Result<UserDto>> LoginAsync(UserLoginDto user)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> RegisterAsync(UserRegisterDto user)
        {
            throw new NotImplementedException();
        }
    }
}
