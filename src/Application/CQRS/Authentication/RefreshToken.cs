using Application.Common.Abstraction.Infrastructure;
using Application.Common.Abstraction.Persistence;
using SharedKernel.Errors;

namespace Application.CQRS.Authentication;

public static class RefreshToken
{
    public record Command : ICommand<ErrorOr<string>>;

    internal sealed class Handler(
        IBudgetDbContext dbContext,
        ICurrentUserService currentUserService,
        ITokenService<User> tokenService) : ICommandHandler<Command, ErrorOr<string>>
    {
        public async ValueTask<ErrorOr<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user is null || !tokenService.IsRefreshTokenValid(user.RefreshToken))
            {
                return UserErrors.Unauthorized;
            }

            var newRefreshToken = tokenService.GenerateRefreshToken(user);
            user.SetRefreshToken(newRefreshToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return tokenService.GenerateAccessToken(user);
        }
    }
}