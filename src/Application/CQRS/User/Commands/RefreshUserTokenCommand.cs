using Application.Dtos.Jwt;

namespace Application.CQRS.User.Commands;

public readonly record struct RefreshUserTokenCommand : ICommand<ResultT<RefreshTokenResponse>>;

public class RefreshUserTokenCommandHandler : ICommandHandler<RefreshUserTokenCommand, ResultT<RefreshTokenResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly ICurrentUserService _currentUserService;

    public RefreshUserTokenCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService,
        ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    }

    public async ValueTask<ResultT<RefreshTokenResponse>> Handle(RefreshUserTokenCommand command,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();

        var user = await _unitOfWork.UserRepository.GetByIdAsync(_currentUserService.GetUserId());
        if (user is null)
        {
            return ErrorFactory.NotFound("User not found");
        }

        if (!_tokenService.IsRefreshTokenValid(user.RefreshToken.Value))
        {
            return ErrorFactory.Unauthorized("Invalid refresh token");
        }

        if (user.Id != userId)
        {
            return ErrorFactory.Unauthorized("Refresh token does not match the user's token");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken(user);

        user.SetRefreshToken(newRefreshToken);
        _unitOfWork.UserRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RefreshTokenResponse(newAccessToken, newRefreshToken);
    }
}