using System.Security.Claims;
using Application.Abstraction.Infrastructure;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Http;

namespace Infrastructure;

public sealed class CurrentUserService(IHttpContextAccessor contextAccessor) : ICurrentUserService
{
    public UserId? GetUserId()
    {
        var userIdString = contextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userIdGuid))
        {
            return null;
        }

        return UserId.From(userIdGuid);
    }
}