using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Common.Entities;
using Domain.Entities.Users.DomainEvents;
using Domain.Entities.Users.ValueObjects;
using Shared.Errors;
using Vogen;

namespace Domain.Entities.Users;

[ValueObject<Guid>]
public readonly partial struct UserId
{
    public static UserId New()
    {
        return From(Guid.CreateVersion7());
    }
}

#nullable disable

public class User : AggregateRoot<UserId>
{
    public const int AccessTokenValidityInHour = 1;
    public const int RefreshTokenValidityInDays = 7;

    private User()
    {
    } // For EF Core

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

    [MaxLength(50)] public string FirstName { get; private set; }
    [MaxLength(50)] public string LastName { get; private set; }

    public Email Email { get; private set; }
    public Password PasswordHash { get; private set; }

    [MaxLength(512)] public string RefreshToken { get; private set; } = null!;
    public DateTime RefreshTokenExpiryTime { get; private set; }
    public bool WantsEmailNotifications { get; private set; }

    public bool IsRefreshTokenValid => DateTime.UtcNow <= RefreshTokenExpiryTime;

    public Account Account { get; } // Navigation property

    public static User TryCreate(string firstName, string lastName, Email email,
        bool wantsEmailNotifications)
    {
        if (string.IsNullOrEmpty(firstName) || firstName.Length > 50 || string.IsNullOrEmpty(lastName) ||
            lastName.Length > 50)
        {
            throw new DomainException(UserErrors.InvalidName);
        }

        return new User(firstName, lastName, email, wantsEmailNotifications);
    }

    public void SetRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(RefreshTokenValidityInDays);
    }

    public void SetPassword(Password newPassword)
    {
        PasswordHash = newPassword;
    }

    public void AddTransaction(Transaction transaction)
    {
        Account.AddTransaction(transaction);
    }

    public IReadOnlyCollection<Transaction> GetTransactions()
    {
        return Account.Transactions;
    }

    public decimal GetBalance()
    {
        return Account.Balance;
    }
}