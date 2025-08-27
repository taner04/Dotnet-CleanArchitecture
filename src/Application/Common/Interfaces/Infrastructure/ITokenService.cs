using Domain.Entities.Users;
using System.Security.Claims;

namespace Application.Common.Interfaces.Infrastructure;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken(User user);
    List<Claim> GetClaims(string token);
    Claim GetClaim(string token, string claimType);
    bool IsRefreshTokenValid(string token);
}