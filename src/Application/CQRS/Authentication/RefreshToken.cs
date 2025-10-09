using Application.Common;
using Application.Common.Abstraction.Infrastructure;
using Application.Common.Abstraction.Persistence;
using Domain.Entities.Users;
using Shared.Errors;

namespace Application.CQRS.Authentication;

public static class RefreshToken
{
    public record Command(string RefreshToken) : ICommand<ErrorOr<string>>;

    internal sealed class Handler(
        UserService userService,
        IApplicationDbContext dbContext,
        ITokenService<User> tokenService) : ICommandHandler<Command, ErrorOr<string>>
    {
        public async ValueTask<ErrorOr<string>> Handle(Command command, CancellationToken cancellationToken)
        {
            var user = await userService.GetCurrentUserAsync(cancellationToken);

            if (user.RefreshToken != command.RefreshToken || !tokenService.IsRefreshTokenValid(command.RefreshToken))
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