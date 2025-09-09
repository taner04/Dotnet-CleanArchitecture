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

    private User(string firstname, string lastname, ValueObjects.Email email, ValueObjects.Password passwordHash)
    {
        Id = UserId.New();
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        PasswordHash = passwordHash;
    }
    
    public static User TryCreate(string firstname, string lastname, string email, string passwordHash)
    {
        if (string.IsNullOrEmpty(firstname))
        {
            throw new ArgumentException("First name cannot be null or empty.", nameof(firstname));
        }
        
        if (string.IsNullOrEmpty(lastname))
        {
            throw new ArgumentException("Last name cannot be null or empty.", nameof(lastname));
        }
        
        var newMail = ValueObjects.Email.From(email); // Validate email
        var newPassword = ValueObjects.Password.From(passwordHash); // Validate password
        
        return new User(firstname, lastname, newMail, newPassword);
    }
    
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public ValueObjects.Email Email { get; private set; }
    public ValueObjects.Password PasswordHash { get; private set; }
    
    public Account Account { get; private set; } = null!; // Navigation property
}