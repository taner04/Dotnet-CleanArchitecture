using Application.Abstraction;
using Infrastructure.Persistence.Data.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SharedKernel;

namespace Infrastructure.Persistence.Data.Application;

public class ApplicationDbContextFactiry : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseNpgsql($"Host=localhost;Database={Constants.ApplicationDb};Username=postgres;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}