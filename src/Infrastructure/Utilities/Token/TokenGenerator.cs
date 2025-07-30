using Application.Common.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Utilities.Token
{
    [ServiceInjection(typeof(ITokenGenerator), ScopeType.AddTransient)]
    public class TokenGenerator : ITokenGenerator
    {
        private const int TokenExpirationTime = 2;
        private const int RefreshTokenExpirationTime = 168;

        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Jwt GenerateToken(User user)
        {
            var jwtSettings = GetJwtSettings();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, user.Email),
                new(ClaimTypes.Email, user.Email)
            };

            var token = CreateJwtToken(claims, jwtSettings, credentials, jwtSettings.AccessTokenExpiration);
            var refreshToken = CreateJwtToken(claims, jwtSettings, credentials, jwtSettings.RefreshTokenExpiration);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenString = tokenHandler.WriteToken(token);
            var refreshTokenString = tokenHandler.WriteToken(refreshToken);

            return new Jwt(tokenString, token.ValidTo, refreshTokenString, refreshToken.ValidTo);
        }

        private static JwtSecurityToken CreateJwtToken(Claim[] claims, JwtSettings jwtSettings, SigningCredentials signingCredentials, int expirationHours)
        {
            return new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(expirationHours),
                signingCredentials: signingCredentials
            );
        }

        private JwtSettings GetJwtSettings()
        {
            return new JwtSettings(
                _configuration["JwtSettings:SecretKey"] ?? throw new ArgumentNullException("The SecretKey was null"),
                _configuration["JwtSettings:Issuer"] ?? throw new ArgumentNullException("The Issuer was null"),
                _configuration["JwtSettings:Audience"] ?? throw new ArgumentNullException("The Audience was null"),
                TokenExpirationTime,
                RefreshTokenExpirationTime
            );
        }
    }
}
