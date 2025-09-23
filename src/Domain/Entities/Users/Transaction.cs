using Domain.Common;
using SharedKernel.Errors;
using Vogen;
using Money = Domain.Entities.Users.ValueObjects.Money;

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

    private Transaction(Money amount, TransactionType type, string description)
    {
        Id = TransactionId.New();
        Amount = amount;
        Type = type;
        Date = DateTime.UtcNow;
        Description = description;
    }

    public AccountId AccountId { get; private set; }
    public Money Amount { get; private set; }
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

        var money = Money.TryFrom(amount);

        return !money.IsSuccess
            ? throw new DomainException(TransactionErrors.InvalidMoney)
            : new Transaction(money.ValueObject, type, description);
    }
}