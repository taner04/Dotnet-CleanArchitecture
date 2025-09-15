namespace Application.Abstraction.Infrastructure;

public interface ICurrentUserService
{
    UserId? GetUserId();
}