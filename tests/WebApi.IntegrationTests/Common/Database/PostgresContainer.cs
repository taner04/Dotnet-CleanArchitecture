using Testcontainers.PostgreSql;

namespace Api.IntegrationTests.Common.Database;

public class PostgresContainer : IAsyncDisposable
{
    private const int MaxRetries = 5;
    private readonly PostgreSqlContainer _postgresSqlContainer = new PostgreSqlBuilder().Build();

    public string ConnectionString => _postgresSqlContainer.GetConnectionString();

    public async ValueTask DisposeAsync()
    {
        await _postgresSqlContainer.StopAsync();
        await _postgresSqlContainer.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public async Task InitializeAsync()
    {
        await StartWithRetry();
    }

    private async Task StartWithRetry()
    {
        var attempt = 0;
        while (attempt < MaxRetries)
        {
            try
            {
                await _postgresSqlContainer.StartAsync();
                return;
            }
            catch (Exception ex)
            {
                attempt++;
                if (attempt >= MaxRetries)
                {
                    throw new Exception($"Failed to start PostgresSQL container after {MaxRetries} attempts.", ex);
                }

                await Task.Delay(2000);
            }
        }
    }
}