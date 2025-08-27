using Application.Common.Interfaces.Infrastructure.Repositories;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository;

public sealed class UserRepository(ApplicationDbContext dbContext)
    : Repository<User, UserId>(dbContext), IUserRepository
{
    public Task<User?> GetByEmailAsync(string email)
    {
        return DbSet.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);
    }
}