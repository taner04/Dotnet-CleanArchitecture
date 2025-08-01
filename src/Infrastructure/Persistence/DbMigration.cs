using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence
{
    public static class DbMigration
    {
        public static void Migrate(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<ApplicationDbContext>();

            if(dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.Migrate();
            }
        }
    }
}
