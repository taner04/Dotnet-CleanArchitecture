using Domain.Entities.Users;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.IntegrationTests.Common;

[Collection("TestingFixtureCollection")]
public abstract class TestingBase : IAsyncLifetime
{
    private readonly IServiceScope _scope;
    private readonly TestingFixture _fixture;
    private readonly BudgetDbContext _dbContext;

    protected Repository<User> UserRepository;
    
    protected TestingBase(TestingFixture fixture)
    {
        _fixture = fixture;
        _scope = _fixture.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<BudgetDbContext>();

        UserRepository = new Repository<User>(_dbContext);
    }
    
    public async Task InitializeAsync()
    {
        await _fixture.SetUpAsync();
    }

    public Task DisposeAsync()
    {
        _scope.Dispose();
        
        return Task.CompletedTask;   
    }

    protected bool CanConnect => _dbContext.Database.CanConnect();
}