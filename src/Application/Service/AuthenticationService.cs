using Application.Common.Interfaces;
using Application.Common.Interfaces.Infrastructure;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Dtos.Jwt;
using Application.Dtos.User;
using Application.Extensions;
using Application.Mapper;
using Domain.DomainEvents.User;
using SharedKernel.Attributes;
using SharedKernel.Response;

namespace Application.Service
{
    [ServiceInjection(typeof(IAuthenticationService), SharedKernel.Enums.ScopeType.AddTransient)]
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidatorFactory _validatorFactory;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthenticationService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
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

            var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(user.Email);
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

            _unitOfWork.UserRepository.Update(existingUser);
            await _unitOfWork.SaveChangesAsync();

            return ResultT<AuthResponse>.Success(existingUser.ToAuthResponse());
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

            var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(user.Email);
            if( existingUser is not null)
            {
                return Result.Failure(
                    ErrorFactory.Conflict("The email is already registered")
                );
            }

            var newUser = user.ToUser(_passwordHasher.HashPassword(user.Password));

            newUser.SetJwt(_tokenGenerator.GenerateToken(newUser));

            newUser.AddDomainEvent(new UserRegisteredDomainEvent(newUser.FirstName, newUser.LastName, newUser.Email));

            _unitOfWork.UserRepository.Add(newUser);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<ResultT<RefreshTokenResponse>> RefreshTokenAsync(GetUserByIdRequest getUserById)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(getUserById.UserId);
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

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return ResultT<RefreshTokenResponse>.Success(new RefreshTokenResponse(
                user.Jwt.Token.Value,
                user.Jwt.RefreshToken.Value
            ));
        }
    }
}
