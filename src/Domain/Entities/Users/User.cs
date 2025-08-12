namespace Domain.Entities.Users
{
    public sealed class User : AggregateRoot<UserId>
    {
#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        private User() { } // for EF Core
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        
        public User(UserId userId, string firstName, string lastName, string email, string passwordHash)
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

        public bool HasValidJwtToken => Jwt.IsTokenExpired;
        public bool HasValidRefreshToken => Jwt.IsRefreshTokenExpired;

        public Jwt Jwt { get; private set; } = null!; // This will be set by the repository or service layer after fetching the user details.

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
    }
}
