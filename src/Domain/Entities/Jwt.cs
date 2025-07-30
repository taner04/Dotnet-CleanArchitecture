namespace Domain.Entities
{
    public sealed class Jwt : Entity<JwtId>
    {
        public Jwt(string token, DateTime expiration, string refreshToken, DateTime refreshTokenExpiration)
        {
            Token = token;
            Expiration = expiration;
            RefreshToken = refreshToken;
            RefreshTokenExpiration = refreshTokenExpiration;
        }

        public string Token { get; init; }
        public DateTime Expiration { get; init; }
        public string RefreshToken { get; init; }
        public DateTime RefreshTokenExpiration { get; init; }

        public UserId UserId { get; init; }
        public User User { get; set; } = null!;
    }
}
