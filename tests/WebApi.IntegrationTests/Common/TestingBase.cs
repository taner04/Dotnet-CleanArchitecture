using System.Net.Http.Headers;
using System.Net.Http.Json;
using Api.IntegrationTests.Common.Database;
using Api.IntegrationTests.Factories;
using Application.CQRS.Authentication;
using Domain.Entities.Users;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Api.IntegrationTests.Common;

[Collection("TestingFixtureCollection")]
public abstract class TestingBase : IAsyncLifetime
{
    private readonly IServiceScope _scope;
    private readonly TestingFixture _fixture;
    private readonly BudgetDbContext _dbContext;
    protected readonly Repository Repository;
    protected readonly IConfiguration Configuration;
    
    protected TestingBase(TestingFixture fixture)
    {
        _fixture = fixture;
        _scope = _fixture.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<BudgetDbContext>();
        Configuration = _scope.ServiceProvider.GetRequiredService<IConfiguration>();
        if (!_dbContext.Database.CanConnect())
        {
            throw new NpgsqlException("Cannot connect to the database");
        }
        
        Repository = new Repository(_dbContext);
    }
    
    public async ValueTask InitializeAsync() => await _fixture.SetUpAsync();

    public ValueTask DisposeAsync()
    {
        _scope.Dispose();
        GC.SuppressFinalize(this);
        
        return ValueTask.CompletedTask;
    }

    protected HttpClient CreateClient() => _fixture.CreateClient();
    
    protected async Task<HttpClient> CreateAuthenticatedClientAsync(string email, string password)
    {
        var client = _fixture.CreateClient();

        var loginResponse = await client.PostAsJsonAsync(
            Routes.Auth.Login, 
            new LoginUser.Query(email, password));

        var token = await loginResponse.Content.ReadAsStringAsync();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return client;
    }
    
    protected static CancellationToken CurrentCancellationToken => TestContext.Current.CancellationToken;
}