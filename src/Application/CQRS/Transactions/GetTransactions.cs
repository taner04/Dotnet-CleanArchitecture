using Application.Abstraction.Infrastructure;
using Application.Abstraction.Persistence;
using Mediator;
using Domain.Entities.Users;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Transactions;

public static class GetTransactions
{
    public record Query : IQuery<ErrorOr<List<TransactionDto>>>;
    
    internal sealed class Handler(
        IBudgetDbContext dbContext,
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
                return Error.NotFound(description: "User not found");
            }

            return user.GetTransactions().Select(TransactionDto.From).ToList();
        }
    }

    public record TransactionDto(decimal Amount, string Type, DateTime Date, string Description)
    {
        public static TransactionDto From(Transaction transaction)
        {
            var transactionAmount = transaction.Amount.Value;
            var type = transaction.Type.ToString();
            
            return new TransactionDto(transactionAmount, type, transaction.Date, transaction.Description);
        }
    }
}