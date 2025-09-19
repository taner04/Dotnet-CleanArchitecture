using Application.Common.Abstraction.Infrastructure;
using Application.Common.Abstraction.Persistence;
using SharedKernel.Errors;

namespace Application.CQRS.Accounts;

public static class GetBalance
{
    public record Query : IQuery<ErrorOr<decimal>>;
    
    internal sealed class Handler(
        IBudgetDbContext dbContext,
        ICurrentUserService currentUserService) : IQueryHandler<Query, ErrorOr<decimal>> 
    {
        public async ValueTask<ErrorOr<decimal>> Handle(Query query, CancellationToken cancellationToken)
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
            
            return user.GetBalance();
        }
    }
}