using Domain.Entities.Users;

namespace Application.Common.Interfaces.Infrastructure.Repositories;

public interface IUserRepository : IRepository<User, UserId>
{
    Task<User?> GetByEmailAsync(string email);
}