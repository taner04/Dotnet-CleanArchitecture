using Application.Abstraction.Utils;
using Application.Dtos.Jwt;
using Domain.Entities.Users;

namespace Application.CQRS.User.RefreshUserToken;

public class RefreshUserTokenCommandHandler : ICommandHandler<RefreshUserTokenCommand, ResultT<RefreshTokenResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public RefreshUserTokenCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async ValueTask<ResultT<RefreshTokenResponse>> Handle(RefreshUserTokenCommand command,
        CancellationToken cancellationToken)
    {
        if (!_tokenService.IsRefreshTokenValid(command.RefreshToken))
            return ResultT<RefreshTokenResponse>.Failure(
                ErrorFactory.Unauthorized("Invalid refresh token"));

        var emailClaim = _tokenService.GetClaim(command.RefreshToken, "email");

        var user = await _unitOfWork.UserRepository.GetByEmailAsync(emailClaim.Value);
        if (user is null)
            return ResultT<RefreshTokenResponse>.Failure(
                ErrorFactory.NotFound("User not found"));

        var subClaim = _tokenService.GetClaim(command.AccessToken, "sub");
        if (user.Jwt.RefreshToken.Value != command.RefreshToken || user.Id != Guid.Parse(subClaim.Value))
            return ResultT<RefreshTokenResponse>.Failure(
                ErrorFactory.Unauthorized("Refresh token does not match the user's token"));

        var newAccessToken = _tokenService.GenerateAccessToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken(user);

        user.SetJwt(new Jwt(newRefreshToken));
        _unitOfWork.UserRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultT<RefreshTokenResponse>.Success(new RefreshTokenResponse(
            newAccessToken,
            newRefreshToken));
    }
}