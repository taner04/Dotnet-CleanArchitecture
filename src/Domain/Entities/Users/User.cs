using Domain.Abstraction;
using Domain.Entities.Base;
using Domain.Entities.Carts;
using Domain.Entities.Orders;
using Domain.ValueObjects;
using UserId = Domain.ValueObjects.Identifiers.UserId;

namespace Domain.Entities.Users;

public sealed class User : AggregateRoot<UserId>, ISoftDeletable
{
    public const int AccessTokenExpirationMinutes = 60; 
    public const int RefreshTokenExpirationDays = 7;  

#pragma warning disable CS8618
    private User() { } // for EFC
#pragma warning restore CS8618

    private User(string firstName, string lastName, string email)
    {
        Id = UserId.New();
        FirstName = firstName;
        LastName = lastName;
        Email = Email.From(email);
        RefreshTokenExpiration = JwtTokenExpiration.From(DateTime.UtcNow.AddDays(7));
    }

    public void SetRefreshToken(string refreshToken)
    {
        RefreshToken = JwtToken.From(refreshToken);
        RefreshTokenExpiration = JwtTokenExpiration.From(DateTime.UtcNow.AddDays(7));
    }
    
    public void SetPasswordHash(string password) => Password = Password.From(password);

    public static User TryCreate(string firstName, string lastName, string email)
    {
        if (string.IsNullOrWhiteSpace(firstName)) throw new DomainException("First name cannot be empty.");
        if (string.IsNullOrWhiteSpace(lastName)) throw new DomainException("Last name cannot be empty.");

        return new User(firstName, lastName, email);
    }

    public bool HasValidRefreshToken => RefreshTokenExpiration.Value > DateTime.UtcNow;

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; } 
    public JwtToken RefreshToken { get; private set; } 
    public JwtTokenExpiration RefreshTokenExpiration { get; private set; }
    public bool IsDeleted { get; set; }
    
    public Cart Cart { get; private set; }  // Navigation property
    public List<Order> Orders { get; private set; } // Navigation property
}