using Domain.Common;
using Domain.Common.Exceptions;
using Vogen;
using Money = Domain.Entities.Users.ValueObjects.Money;

namespace Domain.Entities.Users;

[ValueObject<Guid>]
public readonly partial struct TransactionId
{
    public static TransactionId New() => From(Guid.CreateVersion7());
}

public enum TransactionType
{
    Income,
    Expense
}

public class Transaction : Entity<TransactionId>
{
    private Transaction() { } // EF

    private Transaction(Money amount, TransactionType type, string description)
    {
        Id = TransactionId.New();
        Amount = amount;
        Type = type;
        Date = DateTime.UtcNow;
        Description = description;
    }

    public static Transaction TryCreate(decimal amount, TransactionType type, string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new DomainException("Description cannot be null or whitespace.");
        }
        
        var money = Money.From(amount); 
        
        return new Transaction(money, type, description);
    }
    
    public AccountId AccountId { get; private set; }
    public Money Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public DateTime Date { get; private set; }
    public string Description { get; private set; }
    
    public Account Account { get; private set; } = null!; // Navigation property
}