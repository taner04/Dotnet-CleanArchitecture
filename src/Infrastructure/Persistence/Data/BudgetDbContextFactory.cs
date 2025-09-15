using Microsoft.EntityFrameworkCore.Design;
using SharedKernel;

namespace Infrastructure.Persistence.Data;

public class BudgetDbContextFactory : IDesignTimeDbContextFactory<BudgetDbContext>
{
    public BudgetDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BudgetDbContext>();
        
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseNpgsql($"Host=localhost;Database={Constants.BudgetDb};Username=postgres;");

        return new BudgetDbContext(optionsBuilder.Options);
    }
}