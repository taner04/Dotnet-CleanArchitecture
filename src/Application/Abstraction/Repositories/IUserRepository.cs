using Domain.Entities.Users;

namespace Application.Abstraction.Repositories;

public interface IUserRepository : IRepository<User, UserId>
{
    Task<User?> GetByEmailAsync(string email);
}