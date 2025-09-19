using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Abstraction.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Utils.Token;

public sealed class TokenService : ITokenService<User>
{
    private readonly IConfiguration _configuration;
    private readonly JwtSettings _jwtSettings;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _jwtSettings = GetJwtSettings();
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public string GenerateAccessToken(User user)
    {
        var claims = GetClaims(user, "access");
        var jwt = GetJwtSecurityToken(claims, DateTime.UtcNow.AddHours(User.AccessTokenValidityInHour));

        return _tokenHandler.WriteToken(jwt);
    }

    public string GenerateRefreshToken(User user)
    {
        var claims = GetClaims(user, "refresh");
        var jwt = GetJwtSecurityToken(claims, DateTime.UtcNow.AddDays(User.RefreshTokenValidityInDays));

        return _tokenHandler.WriteToken(jwt);
    }

    public bool IsRefreshTokenValid(string token)
    {
        var validationParameters = GetValidationParameters();

        try
        {
            var principal = _tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            var scope = principal.Claims.FirstOrDefault(c => c.Type == "scope")?.Value;
            return string.Equals(scope, "refresh", StringComparison.Ordinal);
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
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new Claim("scope", scope)
        ];
    }

    private JwtSecurityToken GetJwtSecurityToken(Claim[] claims, DateTime expires)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
            SecurityAlgorithms.HmacSha256
        );

        return new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            DateTime.UtcNow,
            expires,
            signingCredentials
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
        return new TokenValidationParameters
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
            ValidAlgorithms = [SecurityAlgorithms.HmacSha256]
        };
    }
}