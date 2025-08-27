using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Extensions;

/// <summary>
/// Provides extension methods for <see cref="DbContext"/>.
/// </summary>
public static class DbContextExtension
{
    /// <summary>
    /// Applies any pending migrations to the database if there are migrations that have not been applied yet.
    /// </summary>
    /// <param name="dbContext">The <see cref="DbContext"/> instance to migrate.</param>
    public static void Migrate(this DbContext dbContext)
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
    }
}