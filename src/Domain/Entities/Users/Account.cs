using Domain.Common;
using Domain.Entities.Users.ValueObjects;
using Vogen;

namespace Domain.Entities.Users;

[ValueObject<Guid>]
public readonly partial struct AccountId
{
    public static AccountId New() => From(Guid.CreateVersion7());
}

public class Account : Entity<AccountId>
{
    private readonly List<Transaction> _transactions = [];
    
    private Account() { } // For EF Core
    
    private Account(UserId userId)
    {
        Id = AccountId.New();
        UserId = userId;
        Balance = Money.From(0); // Initial balance is zero
    }
    
    public void AddTransaction(Transaction transaction) => _transactions.Add(transaction);

    public UserId UserId { get; private set; }
    public Money Balance { get; private set; } 
    
    public User User { get; private set; } = null!; // Navigation property
    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly(); // Navigation property
}