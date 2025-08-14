using Application.Common.Interfaces.Infrastructure;
using Domain.Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Utilities.Token
{
    [ServiceInjection(typeof(ITokenService), ScopeType.AddTransient)]
    public sealed class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        private static readonly HashSet<string> ClaimsToExtract = new(StringComparer.OrdinalIgnoreCase)
        {
            JwtRegisteredClaimNames.Sub,
            JwtRegisteredClaimNames.Email,
            JwtRegisteredClaimNames.Jti,
            JwtRegisteredClaimNames.Iat,
            "scope"
        };

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtSettings = GetJwtSettings();
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public string GenerateAccessToken(User user)
        {
            var claims = GetClaims(user, "access");
            var jwt = GetJwtSecurityToken(claims, DateTime.UtcNow.AddMinutes(Jwt.AccessTokenExpirationMinutes));

            return _tokenHandler.WriteToken(jwt);
        }

        public string GenerateRefreshToken(User user)
        {
            var claims = GetClaims(user, "refresh");
            var jwt = GetJwtSecurityToken(claims, DateTime.UtcNow.AddDays(Jwt.RefreshTokenExpirationDays));

            return _tokenHandler.WriteToken(jwt);
        }

        public Claim GetClaim(string token, string claimType)
        {
            var jwtSecurityToken = _tokenHandler.ReadJwtToken(token) ?? throw new ArgumentException("Invalid token format.");
            var claim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type.Equals(claimType));

            return claim ?? throw new ArgumentException($"Claim '{claimType}' not found in the token.");
        }

        public List<Claim> GetClaims(string token)
        {
            var jwtSecurityToken = _tokenHandler.ReadJwtToken(token) ?? throw new ArgumentException("Invalid token format.");
            return [.. jwtSecurityToken.Claims.Where(c => ClaimsToExtract.Contains(c.Type))];
        }

        public bool IsRefreshTokenValid(string token)
        {
            var validationParameters = GetValidationParameters();

            try
            {
                var principal = _tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                
                var scope = principal.Claims.FirstOrDefault(c => c.Type == "scope")?.Value;
                if (!string.Equals(scope, "refresh", StringComparison.Ordinal)) return false;
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static Claim[] GetClaims(User user, string scope)
        {
            return
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim("scope", scope),
            ];
        }

        private JwtSecurityToken GetJwtSecurityToken(Claim[] claims, DateTime expires)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                SecurityAlgorithms.HmacSha256
            );
            
            return new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: signingCredentials
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

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                // Issuer/Audience
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                ValidateIssuer = true,
                ValidateAudience = true,

                // Signature
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),

                // Expiration
                ValidateLifetime = true,      
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.FromMinutes(2),

                // Algo
                ValidAlgorithms = [SecurityAlgorithms.HmacSha256],
            };
        }
    }
}
