using Aspire.Hosting;
using Microsoft.Extensions.Logging;
using Projects;

namespace IntegrationTest.Common;

[Collection("Aspire")]
public class AspireAppFixture : IAsyncLifetime
{
    private DistributedApplication? _app;
    private IDistributedApplicationTestingBuilder? _appHost;

    private CancellationToken CancellationToken { get; set; }

    public HttpClient ApiClient { get; private set; } = null!;

    private static TimeSpan DefaultTimeout => TimeSpan.FromMinutes(5);

    public async Task InitializeAsync()
    {
        var cancellationToken = new CancellationTokenSource(DefaultTimeout);
        CancellationToken = cancellationToken.Token;

        _appHost = await DistributedApplicationTestingBuilder.CreateAsync<AppHost>(CancellationToken);
        _appHost.Services.AddLogging(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Debug);
            // Override the logging filters from the app's configuration
            logging.AddFilter(_appHost.Environment.ApplicationName, LogLevel.Debug);
            logging.AddFilter("Aspire.", LogLevel.Debug);
            // To output logs to the xUnit.net ITestOutputHelper, consider adding a package from https://www.nuget.org/packages?q=xunit+logging
        });

        _appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        _app = await _appHost.BuildAsync(CancellationToken).WaitAsync(DefaultTimeout, CancellationToken);
        await _app.StartAsync(CancellationToken).WaitAsync(DefaultTimeout, CancellationToken);

        ApiClient = _app.CreateHttpClient("api");
        await _app.ResourceNotifications.WaitForResourceAsync("api").WaitAsync(DefaultTimeout, CancellationToken);
    }

    public async Task DisposeAsync() => await _app!.DisposeAsync();
}
