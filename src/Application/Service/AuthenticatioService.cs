using Application.Common.Interfaces.Infrastructure;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Dtos.User;
using Application.Response;
using Domain.Entities;
using FluentValidation;
using SharedKernel.Attributes;

namespace Application.Service
{
    [ServiceInjection(typeof(IAuthenticationService), SharedKernel.Enums.ScopeType.AddTransient)]
    public sealed class AuthenticatioService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserRegisterDto> _userRegisterValidator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthenticatioService(
            IUserRepository userRepository, 
            IValidator<UserRegisterDto> userRegisterValidator, 
            IPasswordHasher passwordHasher, 
            ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _userRegisterValidator = userRegisterValidator;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Result<UserDto>> LoginAsync(UserLoginDto user)
        {
            var foundUser = await _userRepository.GetByEmailAsync(user.Email);

            if(foundUser is null)
            {
                return Result<UserDto>.Failure(ErrorFactory.NotFound("The Email does not exist"));
            }

            if(!_passwordHasher.VerifyPassword(user.Password, foundUser.Password))
            {
                 return Result<UserDto>.Failure(ErrorFactory.Unauthorized("Invalid credantials provided"));
            }

            if(foundUser.Jwt!.Expiration >= DateTime.UtcNow || foundUser.Jwt!.RefreshTokenExpiration >= DateTime.UtcNow)
            {
                foundUser.Jwt = _tokenGenerator.GenerateToken(foundUser);
                _userRepository.Update(foundUser);
                await _userRepository.DbContext.SaveChangesAsync();
            }

            return Result<UserDto>.Success(new(foundUser.Id.Value, foundUser.Firstname, foundUser.Lastname, foundUser.Email, foundUser.Jwt.Token));
        }

        public async Task<Result<bool>> RegisterAsync(UserRegisterDto user)
        {
            var validatioResult = _userRegisterValidator.Validate(user);
            if (!validatioResult.IsValid)
            {
                return Result<bool>.Failure(
                    ErrorFactory.ValidationError("Some properties are missing", (Dictionary<string, string[]>)validatioResult.ToDictionary())
                );
            }

            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if (existingUser is not null)
            {
                return Result<bool>.Failure(ErrorFactory.Conflict("The Email already exists"));
            }

            var mappedUser = new User(user.Firstname, user.Lastname, user.Email, _passwordHasher.HashPassword(user.Password));
            var generatedJwt = _tokenGenerator.GenerateToken(mappedUser);

            mappedUser.Jwt = generatedJwt;
            _userRepository.Add(mappedUser);

            await _userRepository.DbContext.SaveChangesAsync();
            
            return Result<bool>.Success(true);
        }
    }
}
