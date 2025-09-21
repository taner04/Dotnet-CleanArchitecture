using Api.IntegrationTests.Common.Database;
using Domain.Entities.Users;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Api.IntegrationTests.Common;

[Collection("TestingFixtureCollection")]
public abstract class TestingBase : IAsyncLifetime
{
    private readonly IServiceScope _scope;
    private readonly TestingFixture _fixture;
    private readonly BudgetDbContext _dbContext;
    protected Repository Repository;
    
    protected TestingBase(TestingFixture fixture)
    {
        _fixture = fixture;
        _scope = _fixture.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<BudgetDbContext>();

        if (!_dbContext.Database.CanConnect())
        {
            throw new NpgsqlException("Cannot connect to the database");
        }
        
        Repository = new Repository(_dbContext);
    }
    
    public async Task InitializeAsync() => await _fixture.SetUpAsync();

    public Task DisposeAsync()
    {
        _scope.Dispose();
        return Task.CompletedTask;   
    }

    protected HttpClient GetApiClient() => _fixture.ApiClient.Value;
}