using Domain.ValueObjects.Identifiers;

namespace Application.Abstraction.Utils;

public interface ICurrentUserService
{
    UserId GetUserId();
    string GetAccessToken();
}