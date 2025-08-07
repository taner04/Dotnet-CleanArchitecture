using Domain.Entities.Users;

namespace Application.Common.Interfaces.Infrastructure
{
    public interface ITokenGenerator
    {
        void GenerateToken(User user);
    }
}
