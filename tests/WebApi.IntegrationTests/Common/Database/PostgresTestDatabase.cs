using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Persistence.Data;

namespace Api.IntegrationTests.Common.Database;

public class PostgresTestDatabase : IAsyncDisposable
{
    private readonly PostgresContainer _postgresContainer = new();
    private readonly List<string> _tableNames = ["Transactions", "Accounts", "Users"];
    private string _connectionString = null!;
    private DbContextOptions<BudgetDbContext> _dbContextOptions = null!;

    public DbConnection DbConnection => new NpgsqlConnection(_postgresContainer.ConnectionString);

    public async ValueTask DisposeAsync()
    {
        await _postgresContainer.DisposeAsync();
        GC.SuppressFinalize(this);
    }

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

        foreach (var sql in _tableNames.Select(tableName => $"Delete from \"{tableName}\""))
        {
            await context.Database.ExecuteSqlRawAsync(sql);
        }
    }
}