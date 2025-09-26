using Domain.Common;
using Domain.Entities.Users.ValueObjects;
using Shared.Errors;
using Vogen;

namespace Domain.Entities.Users;

[ValueObject<Guid>]
public readonly partial struct AccountId
{
    public static AccountId New()
    {
        return From(Guid.CreateVersion7());
    }
}

public class Account : Entity<AccountId>
{
    private readonly List<Transaction> _transactions = [];

    private Account()
    {
    } // For EF Core

    public Account(UserId userId)
    {
        Id = AccountId.New();
        UserId = userId;
        Balance = Money.From(0); // Initial balance is zero
    }

    public UserId UserId { get; private set; }
    public Money Balance { get; private set; }

    public User User { get; private set; } = null!; // Navigation property
    public IReadOnlyList<Transaction> Transactions => _transactions.ToList(); // Navigation property

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
        UpdateBalance(transaction.Amount.Value, transaction.Type);
    }

    private void UpdateBalance(decimal amount, TransactionType type)
    {
        var newBalanceResult = type switch
        {
            TransactionType.Income => Money.TryFrom(Balance.Value + amount),
            TransactionType.Expense => Money.TryFrom(Balance.Value - amount),
            _ => throw new DomainException(TransactionErrors.InvalidTransactionType)
        };

        Balance = newBalanceResult.ValueObject;
    }
}