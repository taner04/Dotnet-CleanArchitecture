using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence.Seed
{
    public static class ApplicationDbMigration
    {
        public static void DatabaseMigration(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }
    }
}
