using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seed
{
    public static class SeedFactory
    {
        public static void SeedData(DbContext dbContext)
        {
            UserSeed.Seed(dbContext);
            
            dbContext.SaveChanges();
        }
    }
}
