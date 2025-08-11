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
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
        }

        public async Task<ResultT<AuthResponse>> LoginAsync(LoginRequest user)
        {
            var validationResult = _validatorFactory.GetResult(user);
            if (!validationResult.IsValid)
            {
                return ResultT<AuthResponse>.Failure(
                    ErrorFactory.ValidationError(validationResult.ToDictionary())
                );
            }

            var existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if (existingUser is null)
            {
                return ResultT<AuthResponse>.Failure(
                    ErrorFactory.NotFound("User not found")
                );
            }

            if (!_passwordHasher.VerifyPassword(user.Password, existingUser.PasswordHash))
            {
                return ResultT<AuthResponse>.Failure(
                    ErrorFactory.Unauthorized("Invalid password")
                );
            }

            existingUser.SetJwt(_tokenGenerator.GenerateToken(existingUser));

            _userRepository.Update(existingUser);
            await _userRepository.DbContext.SaveChangesAsync();

            return ResultT<AuthResponse>.Success(new AuthResponse(
                existingUser.Id.Value,
                existingUser.FirstName,
                existingUser.LastName,
                existingUser.Email,
                existingUser.Jwt.Token.Value,
                existingUser.Jwt.RefreshToken.Value
            ));
        }

        public async Task<Result> RegisterAsync(RegisterRequest user)
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
        public async Task<ResultT<RefreshTokenResponse>> RefreshTokenAsync(UserId userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
            {
                return ResultT<RefreshTokenResponse>.Failure(
                    ErrorFactory.NotFound("User not found")
                );
            }

            if(!user.HasValidRefreshToken)
            {
                return ResultT<RefreshTokenResponse>.Failure(
                    ErrorFactory.Unauthorized("Refresh token is invalid or expired. Please relogin.")
                );
            }

            user.SetJwt(_tokenGenerator.GenerateToken(user));

            _userRepository.Update(user);
            await _userRepository.DbContext.SaveChangesAsync();

            return ResultT<RefreshTokenResponse>.Success(new RefreshTokenResponse(
                user.Jwt.Token.Value,
                user.Jwt.RefreshToken.Value
            ));
        }
    }
}
