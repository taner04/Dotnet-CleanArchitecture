using Domain.Common.Interfaces;
using Domain.Exceptions;

namespace Domain.Entities.Users
{
    public sealed class User : AggregateRoot<UserId>, IAuditable, ISoftDeletable
    {
#pragma warning disable CS8618 
        private User() { } // for EF Core
#pragma warning restore CS8618 
        
        private User(UserId userId, string firstName, string lastName, string email, string passwordHash)
        {
            Id = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
        }

        public void SetJwt(Jwt jwt)
        {
            Jwt = jwt ?? throw new ArgumentNullException(nameof(jwt));
        }
        
        public static User TryCreate(UserId userId, string firstName, string lastName, string email, string passwordHash)
        {
            if (userId == Guid.Empty)
            {
                throw new InvalidIdException();
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty.", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));
            }
            
            return new User(userId, firstName, lastName, email, passwordHash);
        }

        public bool HasValidRefreshToken => !Jwt.IsRefreshTokenExpired;

        public Jwt Jwt { get; private set; } = null!; // This will be set by the repository or service layer after fetching the user details.

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
