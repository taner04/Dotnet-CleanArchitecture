using Domain.Entities.Users;
using UserId = Domain.ValueObjects.Identifiers.UserId;

namespace Application.Abstraction.Repositories;

public interface IUserRepository : IRepository<User, UserId>
{
    Task<User?> GetByEmailAsync(string email);
}