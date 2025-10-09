using System.Security.Claims;
using Application.Common.Abstraction.Infrastructure;
using Microsoft.AspNetCore.Http;
using UserId = Domain.Entities.Users.UserId;

namespace Infrastructure.Utils;

public sealed class CurrentUserService(IHttpContextAccessor contextAccessor) : ICurrentUserService
{
    public UserId? GetUserId()
    {
        var userIdString = contextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userIdGuid))
        {
            return null;
        }

        return UserId.From(userIdGuid);
    }
}