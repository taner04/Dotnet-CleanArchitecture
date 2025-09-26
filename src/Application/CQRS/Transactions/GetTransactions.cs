using Application.Common.Abstraction.Infrastructure;
using Application.Common.Abstraction.Persistence;
using Application.Mapper;
using Shared.Errors;

namespace Application.CQRS.Transactions;

public static class GetTransactions
{
    public record Query : IQuery<ErrorOr<List<TransactionDto>>>;

    internal sealed class Handler(
        IApplicationDbContext dbContext,
        ICurrentUserService currentUserService) : IQueryHandler<Query, ErrorOr<List<TransactionDto>>>
    {
        public async ValueTask<ErrorOr<List<TransactionDto>>> Handle(Query query, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();

            var user = await dbContext.Users.Where(x => x.Id == userId)
                .Include(x => x.Account)
                .ThenInclude(a => a.Transactions)
                .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                return UserErrors.Unauthorized;
            }

            return user.GetTransactions().Select(t => t.ToDto()).ToList();
        }
    }
    
    // ReSharper disable once ClassNeverInstantiated.Global
    public record TransactionDto(decimal Amount, string Type, DateTime Date, string Description);
}