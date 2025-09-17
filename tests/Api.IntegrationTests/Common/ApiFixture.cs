using Application.Abstraction.Persistence;
using Aspire.Hosting;
using Aspire.Hosting.Testing;
using Infrastructure.Persistence.Data;

namespace Api.IntegrationTests.Common;

[CollectionDefinition("ApiCollection")]
public class ApiFixtureCollection : ICollectionFixture<ApiFixture>;

// ReSharper disable once ClassNeverInstantiated.Global
public class ApiFixture(TestDatabase testDatabase) : IAsyncLifetime
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(120);
    private DistributedApplication _app = null!;
    private HttpClient? _apiClient;
    
    public HttpClient ApiClient => _apiClient ??= CreateClient(SharedKernel.AspireConstants.BudgetApi);
    
    public async Task InitializeAsync()
    {
        var cancellationToken = new CancellationTokenSource(DefaultTimeout).Token;
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.AppHost>(cancellationToken);
        appHost.Services.AddLogging(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Debug);
            // Override the logging filters from the app's configuration
            logging.AddFilter(appHost.Environment.ApplicationName, LogLevel.Debug);
            logging.AddFilter("Aspire.", LogLevel.Debug);
            // To output logs to the xUnit.net ITestOutputHelper, consider adding a package from https://www.nuget.org/packages?q=xunit+logging
        });
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        appHost.Services.AddDbContext<IBudgetDbContext, BudgetDbContext>();
        
        _app = await appHost.BuildAsync(cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);
        await _app.StartAsync(cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);
    }

    public async Task DisposeAsync()
    {
        await _app.DisposeAsync();
    }
    
    
    private HttpClient CreateClient(string resourceName)
        => _app.CreateHttpClient(resourceName);
}