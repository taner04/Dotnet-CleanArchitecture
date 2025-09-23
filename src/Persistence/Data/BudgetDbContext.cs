using Application.Common.Abstraction.Persistence;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Converter;

namespace Persistence.Data;

public sealed class BudgetDbContext(DbContextOptions<BudgetDbContext> options)
    : DbContext(options), IBudgetDbContext
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BudgetDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.RegisterAllInIdConverter();
        configurationBuilder.RegisterAllInValueObjectConverter();
    }
}