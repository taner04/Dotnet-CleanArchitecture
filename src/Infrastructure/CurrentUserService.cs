using System.Security.Claims;
using Application.Abstraction.Utils;
using Domain.ValueObjects.Identifiers;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Infrastructure;

[ServiceInjection(typeof(ICurrentUserService), ScopeType.Scoped)]
public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    
    public UserId GetUserId()
    {
        var userIdString = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userIdGuid))
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        return UserId.From(userIdGuid);
    }

    public string GetAccessToken()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
        if (authorizationHeader == null || !authorizationHeader.Contains("Bearer"))
        {
            throw new UnauthorizedAccessException("Authorization header is missing or invalid.");
        }
        
        var token = authorizationHeader.Split(" ").Last();
        var handler = new JsonWebTokenHandler();
        var jwtToken = handler.ReadJsonWebToken(token);
        
        return jwtToken == null ? throw new UnauthorizedAccessException("Invalid JWT token.") : token;
    }
}