using Application.Common.Interfaces.Infrastructure;
using Domain.Entities.Users;
using Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Utilities.Token
{
    [ServiceInjection(typeof(ITokenGenerator), ScopeType.AddTransient)]
    public sealed class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Jwt GenerateToken(User user)
        {
            var jwtSettings = GetJwtSettings();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Jwt.TokenExpirationMinutes),
                signingCredentials: credentials
            );

            var refreshToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(Jwt.RefreshTokenExpirationDays),
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenString = tokenHandler.WriteToken(token);
            var refreshTokenString = tokenHandler.WriteToken(refreshToken);

            return new Jwt(
                JwtToken.From(tokenString), 
                JwtToken.From(refreshTokenString)
            );
        }

        private JwtSettings GetJwtSettings()
        {
            return new JwtSettings(
                _configuration["JwtSettings:SecretKey"] ?? throw new ArgumentNullException("The SecretKey was null"),
                _configuration["JwtSettings:Issuer"] ?? throw new ArgumentNullException("The Issuer was null"),
                _configuration["JwtSettings:Audience"] ?? throw new ArgumentNullException("The Audience was null")
            );
        }
    }
}
