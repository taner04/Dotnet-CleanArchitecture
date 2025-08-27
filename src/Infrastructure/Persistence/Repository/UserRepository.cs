using Application.Common.Interfaces.Infrastructure.Repositories;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public sealed class UserRepository : Repository<User, UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public Task<User?> GetByEmailAsync(string email) 
            => DbSet.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);
    }
}
