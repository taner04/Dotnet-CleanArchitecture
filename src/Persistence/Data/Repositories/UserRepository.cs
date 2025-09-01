using Application.Abstraction.Repositories;
using Domain.Entities.Users;
using Domain.ValueObjects.Identifiers;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Repositories;

public sealed class UserRepository(ApplicationDbContext dbContext)
    : Repository<User, UserId>(dbContext), IUserRepository
{
    public Task<User?> GetByEmailAsync(string email)
    {
        return DbSet.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);
    }
}