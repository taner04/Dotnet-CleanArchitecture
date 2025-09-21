using Application.Common.Abstraction.Persistence;
using Infrastructure.Persistence.Data;

namespace Api.IntegrationTests.Common;

public class Repository(BudgetDbContext budgetDbContext)
{
    public async Task AddAsync<T>(T entity) where T : class
    {
        budgetDbContext.Set<T>().Add(entity);
        await budgetDbContext.SaveChangesAsync();
    }
}