using Domain.Common;
using Vogen;

namespace Domain.Entities.Users;

[ValueObject<Guid>]
public readonly partial struct UserId
{
    public static UserId New() => From(Guid.CreateVersion7());
}


public class User : AggregateRoot<UserId>
{
    private User() { } // For EF Core

    private User(UserId userId, Account account)
    {
        Id = userId;
        Account = account;
    }
    
    public Account Account { get; private set; } // Navigation property
}