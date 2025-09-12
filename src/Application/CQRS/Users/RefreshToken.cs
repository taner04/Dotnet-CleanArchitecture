using Application.Abstraction.Infrastructure;
using Application.Abstraction.Persistence;
using ErrorOr;
using FluentValidation;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users;

public static class RefreshToken
{
    public record Command : ICommand<ErrorOr<string>>;
    
    internal class QueryHandler(
        IBudgetDbContext dbContext,
        ICurrentUserService currentUserService,
        ITokenService<Domain.Entities.Users.User> tokenService) : ICommandHandler<Command, ErrorOr<string>>
    {
        public async ValueTask<ErrorOr<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            if (user is null || !tokenService.IsRefreshTokenValid(user.RefreshToken))
            {
                return Error.Unauthorized(description: "Invalid refresh token, please login again");
            }
            
            var newRefreshToken = tokenService.GenerateRefreshToken(user);
            user.SetRefreshToken(newRefreshToken);
            
            await dbContext.SaveChangesAsync(cancellationToken);
            
            return tokenService.GenerateAccessToken(user);
        }
    }
}