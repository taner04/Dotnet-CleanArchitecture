using System.Net.Http.Headers;
using Application.CQRS.Authentication;
using Npgsql;
using Persistence.Data;
using Shared.WebApi;
using WebApi.IntegrationTests.Common.Database;
using WebApi.IntegrationTests.Factories;

namespace WebApi.IntegrationTests.Common;

[Collection("TestingFixtureCollection")]
public abstract class TestingBase : IAsyncLifetime
{
    private readonly ApplicationDbContext _dbContext;
    private readonly TestingFixture _fixture;
    private readonly IServiceScope _scope;
    protected readonly Repository Repository;

    protected TestingBase(TestingFixture fixture)
    {
        _fixture = fixture;
        _scope = _fixture.CreateScope();

        _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (!_dbContext.Database.CanConnect())
        {
            throw new NpgsqlException("Cannot connect to the database");
        }

        Repository = new Repository(_dbContext);
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

        var registerCommand = new RegisterUser.Command("John", "Doe", UserFactory.Email, UserFactory.Pwd, true);
        await client.PostAsJsonAsync(Routes.Authentication.Register, registerCommand, CurrentCancellationToken);

        var loginResponse = await client.PostAsJsonAsync(
            Routes.Authentication.Login,
            new LoginUser.Query(UserFactory.Email, UserFactory.Pwd));

        var token = await loginResponse.Content.ReadAsStringAsync();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return client;
    }
}