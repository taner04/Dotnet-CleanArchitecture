using Application.Abstraction.Persistence;

namespace Api.IntegrationTests.Common;

public class TestDatabase(IBudgetDbContext dbContext)
{
    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.Database.EnsureDeletedAsync(cancellationToken);
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
    }
}