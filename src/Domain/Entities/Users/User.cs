using Domain.Common.Interfaces;
using Domain.Exceptions;

namespace Domain.Entities.Users;

/// <summary>
/// Represents a user entity with authentication and auditing capabilities.
/// </summary>
public sealed class User : AggregateRoot<UserId>, IAuditable, ISoftDeletable
{
    /// <summary>
    /// Private constructor for EF Core.
    /// </summary>
    private User()
    {
    }

    /// <summary>
    /// Private constructor to initialize a user with required properties.
    /// </summary>
    /// <param name="userId">Unique identifier for the user.</param>
    /// <param name="firstName">User's first name.</param>
    /// <param name="lastName">User's last name.</param>
    /// <param name="email">User's email address.</param>
    /// <param name="passwordHash">Hashed password.</param>
    private User(UserId userId, string firstName, string lastName, string email, string passwordHash)
    {
        Id = userId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
    }

    /// <summary>
    /// Sets the JWT for the user.
    /// </summary>
    /// <param name="jwt">JWT token object.</param>
    /// <exception cref="ArgumentNullException">Thrown if jwt is null.</exception>
    public void SetJwt(Jwt jwt)
    {
        Jwt = jwt ?? throw new ArgumentNullException(nameof(jwt));
    }

    /// <summary>
    /// Attempts to create a new user instance with validation.
    /// </summary>
    /// <param name="userId">Unique identifier for the user.</param>
    /// <param name="firstName">User's first name.</param>
    /// <param name="lastName">User's last name.</param>
    /// <param name="email">User's email address.</param>
    /// <param name="passwordHash">Hashed password.</param>
    /// <returns>A new <see cref="User"/> instance if validation passes.</returns>
    /// <exception cref="InvalidIdException">Thrown if userId is empty.</exception>
    /// <exception cref="ArgumentException">Thrown if any required field is empty.</exception>
    public static User TryCreate(UserId userId, string firstName, string lastName, string email, string passwordHash)
    {
        if (userId == Guid.Empty) throw new InvalidIdException();

        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty.", nameof(email));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));

        return new User(userId, firstName, lastName, email, passwordHash);
    }

    /// <summary>
    /// Indicates whether the user's refresh token is valid.
    /// </summary>
    public bool HasValidRefreshToken => !Jwt.IsRefreshTokenExpired;

    /// <summary>
    /// JWT token associated with the user.
    /// </summary>
    public Jwt Jwt { get; private set; } =
        null!; // This will be set by the repository or service layer after fetching the user details.

    /// <summary>
    /// User's first name.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// User's last name.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// User's email address.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// User's password hash.
    /// </summary>
    public string PasswordHash { get; private set; }

    /// <summary>
    /// Date and time when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date and time when the user was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Indicates whether the user is soft deleted.
    /// </summary>
    public bool IsDeleted { get; set; }
}