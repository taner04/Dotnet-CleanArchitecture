using Application.Common.Abstraction.Infrastructure;
using Application.Common.Abstraction.Persistence;
using SharedKernel.Errors;

namespace Application.CQRS.Users;

public static class GetUserData
{
    public record Query : IQuery<ErrorOr<UserDataDto>>;
    
    internal sealed class Handler(
        IBudgetDbContext dbContext,
        ICurrentUserService currentUserService) : IQueryHandler<Query, ErrorOr<UserDataDto>>
    {
        public async ValueTask<ErrorOr<UserDataDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();
            
            var user = await dbContext.Users.Where(u => u.Id == userId)
                                            .Include(u => u.Account)
                                            .ThenInclude(a => a.Transactions)
                                            .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                return UserErrors.Unauthorized;
            }

            return new UserDataDto(user.FirstName, user.LastName, user.Email.Value);
        }
    }
    
    public record UserDataDto(string FirstName, string LastName, string Email);
}