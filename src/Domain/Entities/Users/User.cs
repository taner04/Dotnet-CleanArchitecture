namespace Domain.Entities.Users
{
    public sealed class User : AggregateRoot<UserId>
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _passwordHash;
        private Jwt jwt = null!;

#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        private User() { } // for EF Core
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        
        public User(UserId userId, string firstName, string lastName, string email, string passwordHash)
        {
            Id = userId;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _passwordHash = passwordHash;
        }

        public void SetJwt(Jwt jwt)
        {
            this.jwt = jwt ?? throw new ArgumentNullException(nameof(jwt));
        }

        public bool HasValidJwtToken => !jwt.IsTokenExpired;
        public bool HasValidRefreshToken => !jwt.IsRefreshTokenExpired;

        public Jwt Jwt => jwt ?? throw new InvalidOperationException("JWT is not set.");

        public string FirstName 
        { 
            get => _firstName; 
            set => _firstName = value; 
        }
        
        public string LastName 
        { 
            get => _lastName; 
            set => _lastName = value; 
        }
        
        public string Email 
        { 
            get => _email; 
            set => _email = value; 
        }
        
        public string PasswordHash 
        { 
            get => _passwordHash; 
            set => _passwordHash = value; 
        }
    }
}
