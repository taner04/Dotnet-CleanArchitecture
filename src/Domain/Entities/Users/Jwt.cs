using Domain.ValueObjects;

namespace Domain.Entities
{
    public sealed class Jwt 
    {
        public const int TokenExpirationMinutes = 60; // 1 hour
        public const int RefreshTokenExpirationDays = 30; // 30 days

        private JwtToken _token;
        private JwtTokenExpiration _tokenExpiration;
        private JwtToken _refreshToken;
        private JwtTokenExpiration _refreshTokenExpiration;

#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        private Jwt() { } // for EF Core
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        
        public Jwt(JwtToken token, JwtToken refreshToken)
        {
            var dateToday = DateTime.UtcNow;

            _token = token;
            _tokenExpiration = JwtTokenExpiration.From(dateToday.AddMinutes(TokenExpirationMinutes));
            _refreshToken = refreshToken;
            _refreshTokenExpiration = JwtTokenExpiration.From(dateToday.AddDays(RefreshTokenExpirationDays));
        }

        public bool IsExpired => DateTime.UtcNow >= _tokenExpiration.Value;

        public JwtToken Token { get => _token; set => _token = value; }
        public JwtTokenExpiration TokenExpiration { get => _tokenExpiration; set => _tokenExpiration = value; }
        public JwtToken RefreshToken { get => _refreshToken; set => _refreshToken = value; }
        public JwtTokenExpiration RefreshTokenExpiration { get => _refreshTokenExpiration; set => _refreshTokenExpiration = value; }
    }
}
