using Application.Abstraction;
using Application.Dtos.Jwt;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Commands;

public readonly record struct RefreshUserTokenCommand : ICommand<ResultT<RefreshTokenResponse>>;

public class RefreshUserTokenCommandHandler : ICommandHandler<RefreshUserTokenCommand, ResultT<RefreshTokenResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ITokenService _tokenService;
    private readonly ICurrentUserService _currentUserService;

    public RefreshUserTokenCommandHandler(IApplicationDbContext dbContext, ITokenService tokenService,
        ICurrentUserService currentUserService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    }

    public async ValueTask<ResultT<RefreshTokenResponse>> Handle(RefreshUserTokenCommand command,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
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
        _dbContext.Users.Update(user);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new RefreshTokenResponse(newAccessToken, newRefreshToken);
    }
}