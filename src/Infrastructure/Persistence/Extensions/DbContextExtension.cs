using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Extensions;

public static class DbContextExtension
{
    public static void Migrate(this DbContext dbContext)
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
    }
}