using System.Data.Common;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn;

namespace Api.IntegrationTests.Common;

public class PostgresTestDatabase : IAsyncDisposable
{
    private readonly PostgresContainer _postgresContainer = new();
    private Respawner _respawner;
    private string _connectionString;
    
    public async Task InitializeAsync()
    {
        await _postgresContainer.InitializeAsync();
        
        var builder = new NpgsqlConnectionStringBuilder(_postgresContainer.ConnectionString);
        
        _connectionString = builder.ConnectionString;

        var budgetDbContextOptions = new DbContextOptionsBuilder<BudgetDbContext>()
            .UseNpgsql(_connectionString)
            .Options;
        
        await using var context = new BudgetDbContext(budgetDbContextOptions);
        await context.Database.MigrateAsync();
        

    }
    
    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }
    
    public async ValueTask DisposeAsync()
    {
        await _postgresContainer.DisposeAsync();
        GC.SuppressFinalize(this);
    }
    
    public DbConnection DbConnection => new NpgsqlConnection(_postgresContainer.ConnectionString);
}