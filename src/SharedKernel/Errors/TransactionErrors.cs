using ErrorOr;

namespace SharedKernel.Errors;

public static class TransactionErrors
{
    public static Error InvalidDescription => Error.Validation(
        code: "Transaction.InvalidDescription",
        description: "The provided description is invalid.");
    
    public static Error InvalidMoney => Error.Validation(
        code: "Transaction.InvalidMoney",
        description: "The provided money amount is invalid.");
    
    public static Error InvalidTransactionType => Error.Validation(
        code: "Transaction.InvalidTransactionType",
        description: "The provided transaction type is invalid.");
}