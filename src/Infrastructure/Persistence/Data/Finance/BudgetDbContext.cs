using Application.Abstraction;
using Domain.Entities.Users;
using Infrastructure.Persistence.Data.Converter;
using Infrastructure.Persistence.Data.Finance.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Data.Finance;

public sealed class BudgetDbContext(DbContextOptions<BudgetDbContext> options)
    : DbContext(options), IBudgetDbContext
{    
    public DbSet<User> Users => Set<User>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        
        configurationBuilder.RegisterAllInIdConverter();
        configurationBuilder.RegisterAllInValueObjectConverter();
    }
}