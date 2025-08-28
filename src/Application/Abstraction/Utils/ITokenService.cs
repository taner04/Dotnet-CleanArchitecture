using System.Security.Claims;
using Domain.Entities.Users;

namespace Application.Abstraction.Utils;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken(User user);
    List<Claim> GetClaims(string token);
    Claim GetClaim(string token, string claimType);
    bool IsRefreshTokenValid(string token);
}