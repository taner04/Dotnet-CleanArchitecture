using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    [ServiceInjection(typeof(IUserRepository), ScopeType.AddTransient)]
    public sealed class UserRepository : Repository<User, UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public Task<User?> GetByEmailAsync(string email) 
            => DbSet.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);
    }
}
