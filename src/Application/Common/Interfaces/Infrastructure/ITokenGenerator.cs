using Domain.Entities.Users;

namespace Application.Common.Interfaces.Infrastructure
{
    public interface ITokenGenerator
    {
        Jwt GenerateToken(User user);
    }
}
