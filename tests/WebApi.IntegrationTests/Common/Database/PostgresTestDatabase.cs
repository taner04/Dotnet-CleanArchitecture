using System.Data.Common;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
namespace Api.IntegrationTests.Common;

public class PostgresTestDatabase : IAsyncDisposable
{
    private readonly List<string> _tableNames = ["Transactions", "Accounts", "Users"];
    private readonly PostgresContainer _postgresContainer = new();
    private string _connectionString;
    private DbContextOptions<BudgetDbContext> _dbContextOptions;
    
    public async Task InitializeAsync()
    {
        await _postgresContainer.InitializeAsync();
        
        var builder = new NpgsqlConnectionStringBuilder(_postgresContainer.ConnectionString);
        
        _connectionString = builder.ConnectionString;

        _dbContextOptions = new DbContextOptionsBuilder<BudgetDbContext>()
            .UseNpgsql(_connectionString)
            .Options;
        
        await using var context = new BudgetDbContext(_dbContextOptions);
        await context.Database.MigrateAsync();
    }
    
    public async Task ResetDatabaseAsync()
    {
        await using var context = new BudgetDbContext(_dbContextOptions);
        
        foreach (var tableName in _tableNames)
        {
            await context.Database.ExecuteSqlRawAsync($"Delete from \"{tableName}\"");
        }
    }
    
    public async ValueTask DisposeAsync()
    {
        await _postgresContainer.DisposeAsync();
        GC.SuppressFinalize(this);
    }
    
    public DbConnection DbConnection => new NpgsqlConnection(_postgresContainer.ConnectionString);
}