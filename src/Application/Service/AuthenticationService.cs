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
        private readonly ITokenService _tokenService;

        public AuthenticationService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
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

            if (!existingUser.HasValidRefreshToken)
            {
                var refreshToken = _tokenService.GenerateRefreshToken(existingUser);
                existingUser.SetJwt(new Jwt(refreshToken));

                _unitOfWork.UserRepository.Update(existingUser);
                await _unitOfWork.SaveChangesAsync();
            }

            var accessToken = _tokenService.GenerateAccessToken(existingUser);
            return ResultT<AuthResponse>.Success(existingUser.ToAuthResponse(accessToken));
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

            var refreshToken = _tokenService.GenerateRefreshToken(newUser);
            newUser.SetJwt(new Jwt(refreshToken));

            newUser.AddDomainEvent(new UserRegisteredDomainEvent(newUser.FirstName, newUser.LastName, newUser.Email));

            _unitOfWork.UserRepository.Add(newUser);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<ResultT<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenDto refreshToken)
        {
            var validationResult = _validatorFactory.GetResult(refreshToken);
            if (!validationResult.IsValid)
            {
                return ResultT<RefreshTokenResponse>.Failure(
                    ErrorFactory.ValidationError(validationResult.ToDictionary())
                );
            }

            if (!_tokenService.IsRefreshTokenValid(refreshToken.RefreshToken))
            {
                return ResultT<RefreshTokenResponse>.Failure(
                    ErrorFactory.Unauthorized("Invalid refresh token")
                );
            }

            var emailClaim = _tokenService.GetClaim(refreshToken.RefreshToken, "email");
            if(emailClaim is null || emailClaim.Value is null)
            {
                return ResultT<RefreshTokenResponse>.Failure(
                    ErrorFactory.Unauthorized("Email claim not found in refresh token")
                );
            }

            var user = await _unitOfWork.UserRepository.GetByEmailAsync(emailClaim.Value);
            if (user is null)
            {
                return ResultT<RefreshTokenResponse>.Failure(
                    ErrorFactory.NotFound("User not found")
                );
            }

            var subClaim = _tokenService.GetClaim(refreshToken.AccessToken, "sub");
            if (user.Jwt.RefreshToken.Value != refreshToken.RefreshToken || user.Id != Guid.Parse(subClaim.Value))
            {
                return ResultT<RefreshTokenResponse>.Failure(
                    ErrorFactory.Unauthorized("Refresh token does not match the user's token")
                );
            }

            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken(user);

            user.SetJwt(new Jwt(newRefreshToken));

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return ResultT<RefreshTokenResponse>.Success(new RefreshTokenResponse(
                newAccessToken,
                newRefreshToken
            ));
        }
    }
}
