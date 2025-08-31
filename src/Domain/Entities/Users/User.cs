using Domain.Abstraction;
using Domain.Entities.Base;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities.Users;

public sealed class User : AggregateRoot<UserId>, ISoftDeletable
{
    public const int AccessTokenExpirationMinutes = 60; 
    public const int RefreshTokenExpirationDays = 7;  

#pragma warning disable CS8618
    private User() { } // for EFC
#pragma warning restore CS8618

    private User(UserId userId, string firstName, string lastName, string email)
    {
        Id = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        RefreshTokenExpiration = DateTime.UtcNow.AddDays(7);
    }

    public void SetRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiration = DateTime.UtcNow.AddDays(RefreshTokenExpirationDays);
    }
    
    public void SetPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));

        PasswordHash = passwordHash;
    }

    public static User TryCreate(UserId userId, string firstName, string lastName, string email)
    {
        if (userId == Guid.Empty) throw new InvalidIdException();

        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

        if (string.IsNullOrWhiteSpace(email)) 
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        return new User(userId, firstName, lastName, email);
    }

    public bool HasValidRefreshToken => RefreshTokenExpiration > DateTime.UtcNow;

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; } = null!;
    public JwtToken RefreshToken { get; private set; } = null!;
    public JwtTokenExpiration RefreshTokenExpiration { get; private set; }
    public bool IsDeleted { get; set; }
}