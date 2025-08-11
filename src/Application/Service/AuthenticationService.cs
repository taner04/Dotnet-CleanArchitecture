using Application.Common.Interfaces;
using Application.Common.Interfaces.Infrastructure;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Dtos.Jwt;
using Application.Dtos.User;
using Application.Response;
using Application.Validator;
using Domain.DomainEvents.User;
using Domain.Entities.Users;
using SharedKernel.Attributes;

namespace Application.Service
{
    [ServiceInjection(typeof(IAuthenticationService), SharedKernel.Enums.ScopeType.AddTransient)]
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidatorFactory _validatorFactory;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthenticationService(IUserRepository userRepository, IValidatorFactory validatorFactory, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _validatorFactory = validatorFactory;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ResultT<UserDto>> LoginAsync(UserLoginDto user)
        {
            var validationResult = _validatorFactory.GetResult(user);
            if (!validationResult.IsValid)
            {
                return ResultT<UserDto>.Failure(
                    ErrorFactory.ValidationError(validationResult.ToDictionary())
                );
            }

            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if (existingUser is null)
            {
                return ResultT<UserDto>.Failure(
                    ErrorFactory.NotFound("User not found")
                );
            }

            if (!_passwordHasher.VerifyPassword(user.Password, existingUser.PasswordHash))
            {
                return ResultT<UserDto>.Failure(
                    ErrorFactory.Unauthorized("Invalid password")
                );
            }

            existingUser.SetJwt(_tokenGenerator.GenerateToken(existingUser));

            _userRepository.Update(existingUser);
            await _userRepository.DbContext.SaveChangesAsync();

            return ResultT<UserDto>.Success(new UserDto(
                existingUser.Id.Value,
                existingUser.FirstName,
                existingUser.LastName,
                existingUser.Email,
                existingUser.Jwt.Token.Value,
                existingUser.Jwt.RefreshToken.Value
            ));
        }

        public async Task<Result> RegisterAsync(UserRegisterDto user)
        {
            var validationResult = _validatorFactory.GetResult(user);
            if (!validationResult.IsValid)
            {
                return Result.Failure(
                    ErrorFactory.ValidationError(validationResult.ToDictionary())
                );
            }

            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if( existingUser is not null)
            {
                return Result.Failure(
                    ErrorFactory.Conflict("The email is already registered")
                );
            }

            var newUser = new User(
                UserId.From(Guid.CreateVersion7()), 
                user.FirstName, 
                user.LastName, 
                user.Email, 
                _passwordHasher.HashPassword(user.Password)
            );

            newUser.SetJwt(_tokenGenerator.GenerateToken(newUser));

            newUser.AddDomainEvent(new UserRegisteredDomainEvent(newUser.FirstName, newUser.LastName, newUser.Email));

            _userRepository.Add(newUser);
            await _userRepository.DbContext.SaveChangesAsync();

            return Result.Success();
        }
        public async Task<ResultT<JwtRefreshedTokenDto>> RefreshTokenAsync(UserId userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
            {
                return ResultT<JwtRefreshedTokenDto>.Failure(
                    ErrorFactory.NotFound("User not found")
                );
            }

            if(!user.HasValidRefreshToken)
            {
                return ResultT<JwtRefreshedTokenDto>.Failure(
                    ErrorFactory.Unauthorized("Refresh token is invalid or expired. Please relogin.")
                );
            }

            user.SetJwt(_tokenGenerator.GenerateToken(user));

            _userRepository.Update(user);
            await _userRepository.DbContext.SaveChangesAsync();

            return ResultT<JwtRefreshedTokenDto>.Success(new JwtRefreshedTokenDto(
                user.Jwt.Token.Value,
                user.Jwt.RefreshToken.Value
            ));
        }
    }
}
