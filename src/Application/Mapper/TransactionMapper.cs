using Application.CQRS.Transactions;
namespace Application.Mapper;

public static class TransactionMapper
{
    public static GetTransactions.TransactionDto ToDto(this Transaction transaction)
    {
        var transactionAmount = transaction.Amount.Value;
        var type = transaction.Type.ToString();

        return new GetTransactions.TransactionDto(transactionAmount, type, transaction.Date, transaction.Description);
    }
}