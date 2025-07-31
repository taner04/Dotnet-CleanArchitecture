
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    [ServiceInjection(typeof(IUserRepository), ScopeType.AddTransient)]
    public sealed class UserRepository : Repository<User, UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetByEmailAsync(string email) 
            => await DbSet.Where(u => u.Email == email).Include(u => u.Jwt).FirstOrDefaultAsync();
    }
}
