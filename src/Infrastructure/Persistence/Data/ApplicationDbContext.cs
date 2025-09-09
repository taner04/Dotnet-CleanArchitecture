using Application.Abstraction;
using Domain.Entities.Users;
using Infrastructure.Persistence.Data.Configuration.Converter;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{    
    public DbSet<User> Users => Set<User>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        
        configurationBuilder.RegisterAllInIdConverter();
        configurationBuilder.RegisterAllInValueObjectConverter();
    }
}