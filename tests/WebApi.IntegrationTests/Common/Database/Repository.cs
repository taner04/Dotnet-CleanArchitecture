using System.Linq.Expressions;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.IntegrationTests.Common.Database;

public class Repository(BudgetDbContext budgetDbContext)
{
    public async Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class
    {
        budgetDbContext.Set<T>().Add(entity);
        await budgetDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<T?> SearchByAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        where T : class
    {
        return await budgetDbContext.Set<T>().Where(expression).FirstOrDefaultAsync(cancellationToken);
    }
}