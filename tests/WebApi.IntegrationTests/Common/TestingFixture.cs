namespace Api.IntegrationTests.Common;

[CollectionDefinition("TestingFixtureCollection")]
public class TestingFixtureCollection : ICollectionFixture<TestingFixture>;

public class TestingFixture : IAsyncLifetime
{
    private readonly PostgresTestDatabase _postgresTestDatabase = new();
    private WebApiFactory _webApiFactory;
    private IServiceScopeFactory _serviceScopeFactory;
    
    public async Task InitializeAsync()
    {
        await _postgresTestDatabase.InitializeAsync();
        _webApiFactory = new WebApiFactory(_postgresTestDatabase.DbConnection);
        _serviceScopeFactory = _webApiFactory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public async Task DisposeAsync()
    {
        await _postgresTestDatabase.DisposeAsync();
        await _webApiFactory.DisposeAsync();
    }
    
    public async Task SetUpAsync() => await _postgresTestDatabase.ResetDatabaseAsync();

    public IServiceScope CreateScope() => _serviceScopeFactory.CreateScope();
    
    public Lazy<HttpClient> ApiClient => new(_webApiFactory.CreateClient());
}