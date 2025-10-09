using Domain.Common;
using Domain.Common.Entities;
using Shared.Errors;
using Vogen;

namespace Domain.Entities.Users;

[ValueObject<Guid>]
public readonly partial struct TransactionId
{
    public static TransactionId New()
    {
        return From(Guid.CreateVersion7());
    }
}

public enum TransactionType
{
    Income,
    Expense
}

public class Transaction : Entity<TransactionId>
{
    private Transaction()
    {
    } // EF

    private Transaction(decimal amount, TransactionType type, string description)
    {
        Id = TransactionId.New();
        Amount = amount;
        Type = type;
        Date = DateTime.UtcNow;
        Description = description;
    }

    public AccountId AccountId { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }

    public Account Account { get; private set; } = null!; // Navigation property

    public static Transaction TryCreate(decimal amount, TransactionType type, string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new DomainException(TransactionErrors.InvalidDescription);
        }

        return new Transaction(amount, type, description);
    }
}