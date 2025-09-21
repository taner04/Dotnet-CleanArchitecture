using Infrastructure.Persistence.Data;

namespace Api.IntegrationTests.Common.Database;

public class Repository(BudgetDbContext budgetDbContext)
{
    public async Task AddAsync<T>(T entity) where T : class
    {
        budgetDbContext.Set<T>().Add(entity);
        await budgetDbContext.SaveChangesAsync();
    }
}