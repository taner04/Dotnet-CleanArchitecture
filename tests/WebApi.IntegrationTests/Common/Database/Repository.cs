using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace WebApi.IntegrationTests.Common.Database;

public class Repository(ApplicationDbContext dbContext)
{
    public async Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class
    {
        dbContext.Set<T>().Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<T?> SearchByAsync<T>(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        where T : class
    {
        return await dbContext.Set<T>().Where(expression).FirstOrDefaultAsync(cancellationToken);
    }
}