using Application.Common;
using Application.Common.Abstraction.Persistence;
using Application.Mapper;

namespace Application.CQRS.Transactions;

public static class GetTransactions
{
    public record Query : IQuery<ErrorOr<List<TransactionDto>>>;

    internal sealed class Handler(
        UserService userService,
        IApplicationDbContext dbContext) : IQueryHandler<Query, ErrorOr<List<TransactionDto>>>
    {
        public async ValueTask<ErrorOr<List<TransactionDto>>> Handle(Query query, CancellationToken cancellationToken)
        {
            var user = await userService.GetCurrentUserWithAccountAndTransactionsAsync(cancellationToken);

            return user.GetTransactions().Select(t => t.ToDto()).ToList();
        }
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public record TransactionDto(decimal Amount, string Type, DateTime Date, string Description);
}