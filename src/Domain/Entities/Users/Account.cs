using Domain.Common;
using Domain.Common.Entities;
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
        Balance = 0; // Initial balance is zero
    }

    public UserId UserId { get; private set; }
    public decimal Balance { get; private set; }

    public User User { get; private set; } = null!; // Navigation property
    public IReadOnlyList<Transaction> Transactions => _transactions.ToList(); // Navigation property

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
        UpdateBalance(transaction.Amount, transaction.Type);
    }

    private void UpdateBalance(decimal amount, TransactionType type)
    {
        Balance = type switch
        {
            TransactionType.Income => Balance + amount,
            TransactionType.Expense => Balance - amount,
            _ => throw new DomainException(TransactionErrors.InvalidTransactionType)
        };
    }
}