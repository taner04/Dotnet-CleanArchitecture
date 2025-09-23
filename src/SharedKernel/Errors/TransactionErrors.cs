using ErrorOr;

namespace SharedKernel.Errors;

public static class TransactionErrors
{
    public static Error InvalidDescription => Error.Validation(
        "Transaction.InvalidDescription",
        "The provided description is invalid.");

    public static Error InvalidMoney => Error.Validation(
        "Transaction.InvalidMoney",
        "The provided money amount is invalid.");

    public static Error InvalidTransactionType => Error.Validation(
        "Transaction.InvalidTransactionType",
        "The provided transaction type is invalid.");
}