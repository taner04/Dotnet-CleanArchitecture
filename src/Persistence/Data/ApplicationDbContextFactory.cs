using Microsoft.EntityFrameworkCore.Design;
using Shared.Aspire;

namespace Persistence.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseNpgsql($"Host=localhost;Database={AspireConstants.ApplicationDb};Username=postgres;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}