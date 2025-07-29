using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Dtos.User;
using Application.Response;
using SharedKernel.Attributes;

namespace Application.Service
{
    [ServiceInjection(typeof(IAuthenticationService), SharedKernel.Enums.ScopeType.AddTransient)]
    public sealed class AuthenticatioService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticatioService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultT<UserDto>> LoginAsync(UserLoginDto user)
        {
            var foundUser = await _userRepository.GetByEmailAsync(user.Email);

            if(foundUser is null)
            {
                return ResultT<UserDto>.Failure("Not found", "Invalid email or password", 404);
            }

            return default!;
        }

        public Task<ResultT<bool>> RegisterAsync(UserRegisterDto user)
        {
            throw new NotImplementedException();
        }
    }
}
