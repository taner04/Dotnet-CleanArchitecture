using System.Net.Http.Headers;
using Application.CQRS.Authentication;
using Newtonsoft.Json;
using Npgsql;
using Persistence.Data;
using Shared.WebApi;
using WebApi.IntegrationTests.Common.Database;
using WebApi.IntegrationTests.Factories;

namespace WebApi.IntegrationTests.Common;

[Collection("TestingFixtureCollection")]
public abstract class TestingBase : IAsyncLifetime
{
    protected readonly ApplicationDbContext DbContext;
    private readonly TestingFixture _fixture;
    private readonly IServiceScope _scope;

    protected TestingBase(TestingFixture fixture)
    {
        _fixture = fixture;
        _scope = _fixture.CreateScope();

        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (!DbContext.Database.CanConnect())
        {
            throw new NpgsqlException("Cannot connect to the database");
        }
    }

    protected static CancellationToken CurrentCancellationToken => TestContext.Current.CancellationToken;

    public async ValueTask InitializeAsync()
    {
        await _fixture.SetUpAsync();
    }

    public ValueTask DisposeAsync()
    {
        _scope.Dispose();
        GC.SuppressFinalize(this);

        return ValueTask.CompletedTask;
    }

    protected HttpClient CreateClient()
    {
        return _fixture.CreateClient();
    }

    protected async Task<HttpClient> CreateAuthenticatedClientAsync()
    {
        var client = _fixture.CreateClient();

        await client.PostAsJsonAsync(
            Routes.Authentication.Register,
            new RegisterUser.Command("John", "Doe", UserFactory.Email, UserFactory.Pwd, true),
            CurrentCancellationToken);

        var response = await client.PostAsJsonAsync(
            Routes.Authentication.Login,
            new LoginUser.Query(UserFactory.Email, UserFactory.Pwd));

        var dto = JsonConvert.DeserializeObject<LoginUser.Dto>(await response.Content.ReadAsStringAsync());
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", dto.AccessToken);

        return client;
    }
}