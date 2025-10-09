using Application.CQRS.Transactions;
using Domain.Entities.Users;

namespace Application.Mapper;

public static class TransactionMapper
{
    public static GetTransactions.TransactionDto ToDto(this Transaction transaction)
    {
        return new GetTransactions.TransactionDto(
            transaction.Amount,
            transaction.Type.ToString(),
            transaction.Date,
            transaction.Description);
    }
}