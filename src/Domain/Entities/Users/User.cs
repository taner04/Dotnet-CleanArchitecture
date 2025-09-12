using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Entities.Users.DomainEvent;
using Domain.Entities.Users.ValueObjects;
using Vogen;

namespace Domain.Entities.Users;

[ValueObject<Guid>]
public readonly partial struct UserId
{
    public static UserId New() => From(Guid.CreateVersion7());
}

#nullable disable

public class User : AggregateRoot<UserId>
{
    public const int AccessTokenValidityInHour = 1;
    public const int RefreshTokenValidityInDays = 7;
    
    private User() { } // For EF Core

    private User(string firstName, string lastName, Email email, bool wantsEmailNotifications)
    {
        Id = UserId.New();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        WantsEmailNotifications = wantsEmailNotifications;
        Account = new Account(Id);
        
        AddDomainEvent(new UserRegisteredDomainEvent(this));
    }

    public static User Create(string firstName, string lastName, Email email, bool wantsEmailNotifications) 
        => new(firstName, lastName,email, wantsEmailNotifications);

    [MaxLength(50)] public string FirstName { get; private set; }
    [MaxLength(50)] public string LastName { get; private set; }
    
    public Email Email { get; private set; }
    public Password PasswordHash { get; private set; }

    [MaxLength(512)] public string RefreshToken { get; private set; } = null!;
    public DateTime RefreshTokenExpiryTime { get; private set; }
    public bool WantsEmailNotifications { get; private set; }
    
    public bool IsRefreshTokenValid => DateTime.UtcNow <= RefreshTokenExpiryTime;
    
    public void SetRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(RefreshTokenValidityInDays);
    }
    
    public void SetPassword(Password newPassword) => PasswordHash = newPassword;
    public void ChangeEmail(Email newEmail) => Email = newEmail;
    public void ChangeEmailNotificationPreference(bool wantsEmailNotifications) => WantsEmailNotifications = wantsEmailNotifications;
    
    public void MakeTransaction(Transaction transaction) => Account.AddTransaction(transaction);

    public Account Account { get; private set; } // Navigation property
}