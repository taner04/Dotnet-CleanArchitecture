using Domain.ValueObjects;

namespace Domain.Entities
{
    public sealed class Jwt 
    {
        public const int TokenExpirationMinutes = 60; // 1 hour
        public const int RefreshTokenExpirationDays = 30; // 30 days


#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        private Jwt() { } // for EF Core
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        
        public Jwt(JwtToken token, JwtToken refreshToken)
        {
            var dateToday = DateTime.UtcNow;

            Token = token;
            TokenExpiration = JwtTokenExpiration.From(dateToday.AddMinutes(TokenExpirationMinutes));
            RefreshToken = refreshToken;
            RefreshTokenExpiration = JwtTokenExpiration.From(dateToday.AddDays(RefreshTokenExpirationDays));
        }

        public bool IsTokenExpired => DateTime.UtcNow >= TokenExpiration.Value;
        public bool IsRefreshTokenExpired => DateTime.UtcNow >= RefreshTokenExpiration.Value;

        public JwtToken Token { get; private set; }
        public JwtTokenExpiration TokenExpiration { get; private set; }
        public JwtToken RefreshToken { get; private set; }
        public JwtTokenExpiration RefreshTokenExpiration { get; private set; }
    }
}
