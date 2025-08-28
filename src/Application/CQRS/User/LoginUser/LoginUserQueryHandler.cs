using Application.Abstraction.Utils;
using Application.Dtos.User;
using Application.Mapper;
using Domain.Entities.Users;

namespace Application.CQRS.User.LoginUser;

public class LoginUserQueryHandler : IQueryHandler<LoginUserQuery, ResultT<AuthResponse>>
{
    private readonly IValidator<LoginUserQuery> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public LoginUserQueryHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async ValueTask<ResultT<AuthResponse>> Handle(LoginUserQuery query, CancellationToken cancellationToken)
    {
        var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(query.Email);
        if (existingUser is null)
            return ResultT<AuthResponse>.Failure(
                ErrorFactory.NotFound("User not found")
            );

        if (!_passwordHasher.VerifyPassword(query.Password, existingUser.PasswordHash))
            return ResultT<AuthResponse>.Failure(
                ErrorFactory.Unauthorized("Invalid password")
            );

        if (!existingUser.HasValidRefreshToken)
        {
            var refreshToken = _tokenService.GenerateRefreshToken(existingUser);
            existingUser.SetJwt(new Jwt(refreshToken));

            _unitOfWork.UserRepository.Update(existingUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        var accessToken = _tokenService.GenerateAccessToken(existingUser);
        return ResultT<AuthResponse>.Success(existingUser.ToAuthResponse(accessToken));
    }
}