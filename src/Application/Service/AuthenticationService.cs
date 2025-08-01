using Application.Common.Interfaces;
using Application.Common.Interfaces.Infrastructure;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Dtos.User;
using Application.Mapper;
using Application.Response;
using Application.Validator;
using SharedKernel.Attributes;

namespace Application.Service
{
    [ServiceInjection(typeof(IAuthenticationService), SharedKernel.Enums.ScopeType.AddTransient)]
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IValidatorFactory _validatorFactory;

        public AuthenticationService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenGenerator tokenGenerator,
            IValidatorFactory validatorFactory)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _validatorFactory = validatorFactory;
        }

        public async Task<Result<UserDto>> LoginAsync(UserLoginDto user)
        {
            var validationResult = _validatorFactory.GetResult(user);
            if (!validationResult.IsValid)
            {
                return Result<UserDto>.Failure(
                    ErrorFactory.ValidationError(validationResult.ToDictionary())
                );
            }
            
            var foundUser = await _userRepository.GetByEmailAsync(user.Email);
            if(foundUser is null)
            {
                return Result<UserDto>.Failure(ErrorFactory.NotFound("The Email does not exist"));
            }

            if(!_passwordHasher.VerifyPassword(user.Password, foundUser.Password))
            {
                 return Result<UserDto>.Failure(ErrorFactory.Unauthorized("Invalid credantials provided"));
            }

            if (foundUser.Jwt == null || foundUser.Jwt.Expiration < DateTime.UtcNow)
            {
                foundUser.Jwt = _tokenGenerator.GenerateToken(foundUser);
                _userRepository.Update(foundUser);
                await _userRepository.DbContext.SaveChangesAsync();
            }

            return Result<UserDto>.Success(UserMapper.ToUserDto(foundUser));
        }

        public async Task<Result<bool>> RegisterAsync(UserRegisterDto user)
        {
            var validatioResult = _validatorFactory.GetResult(user);
            if (!validatioResult.IsValid)
            {
                return Result<bool>.Failure(
                    ErrorFactory.ValidationError(validatioResult.ToDictionary())
                );
            }

            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if (existingUser is not null)
            {
                return Result<bool>.Failure(ErrorFactory.Conflict("The Email already exists"));
            }

            var mappedUser = new User(user.Firstname, user.Lastname, user.Email, _passwordHasher.HashPassword(user.Password));
            mappedUser.Jwt = _tokenGenerator.GenerateToken(mappedUser);
            
            _userRepository.Add(mappedUser);
            await _userRepository.DbContext.SaveChangesAsync();
            
            return Result<bool>.Success(true);
        }
    }
}
