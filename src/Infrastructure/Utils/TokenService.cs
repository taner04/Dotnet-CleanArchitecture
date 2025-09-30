using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Abstraction.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.WebApi;

namespace Infrastructure.Utils;

public sealed class TokenService(IConfiguration configuration) : ITokenService<User>
{
    private readonly JwtSettings _jwtSettings = configuration.GetSection("JWTSettings").Get<JwtSettings>() ?? throw new InvalidOperationException("JWT settings are not configured properly.");
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

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
        try
        {
            var principal = _tokenHandler.ValidateToken(token, _jwtSettings.ToTokenValidationParameters(), out var validatedToken);

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
}