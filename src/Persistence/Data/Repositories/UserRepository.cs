using Application.Abstraction.Repositories;
using Domain.Entities.Users;
using Domain.Identifiers;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Repository;

public sealed class UserRepository(ApplicationDbContext dbContext)
    : Repository<User, UserId>(dbContext), IUserRepository
{
    public Task<User?> GetByEmailAsync(string email)
    {
        return DbSet.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);
    }
}