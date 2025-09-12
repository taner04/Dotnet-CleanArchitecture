using System.Reflection;
using Application.Abstraction;
using Application.Abstraction.Persistence;
using Domain.Entities.Users;
using Infrastructure.Persistence.Data.Configuration;
using Infrastructure.Persistence.Data.Converter;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Data;

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