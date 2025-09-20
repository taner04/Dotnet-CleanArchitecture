using Infrastructure.Persistence.Data;

namespace Api.IntegrationTests.Common;

public class Repository<TEntity>(BudgetDbContext dbContext)
    where TEntity : class
{
    public async Task Add(TEntity entity)
    {
        await dbContext.Set<TEntity>().AddAsync(entity);
        await SaveChangesAsync();
    }

    public async Task AddRange(IEnumerable<TEntity> entities)
    {
        await dbContext.Set<TEntity>().AddRangeAsync(entities);
        await SaveChangesAsync();
    }
    
    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}