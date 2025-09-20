using SharedKernel;
using Testcontainers.PostgreSql;

namespace Api.IntegrationTests.Common;

public class PostgresContainer : IAsyncDisposable
{
    private readonly PostgreSqlContainer _postgresSqlContainer = new PostgreSqlBuilder()
        .WithName($"Dotnet-CleanArchitecture-IntegrationTests-{Guid.NewGuid()}")
        .WithAutoRemove(true)
        .Build();

    public async Task InitializeAsync()
    {
        await _postgresSqlContainer.StartAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _postgresSqlContainer.StopAsync();
        await _postgresSqlContainer.DisposeAsync();
        GC.SuppressFinalize(this);
    }
    
    public string ConnectionString => _postgresSqlContainer.GetConnectionString();
}