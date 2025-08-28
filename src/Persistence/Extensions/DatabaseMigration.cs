using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Data;

namespace Persistence.Extensions;

public static class DatabaseMigration
{
    public static void Migrate(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        
        var migrations = dbContext.Database.GetPendingMigrations();
        if (!migrations.Any()) return;
        
        dbContext.Database.EnsureDeleted();
        dbContext.Database.Migrate();
    }
}