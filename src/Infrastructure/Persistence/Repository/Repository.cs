using Application.Common.Interfaces.Infrastructure.Repositories;
using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository;

public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : struct
{
    protected DbSet<TEntity> DbSet { get; init; }
    public DbContext DbContext { get; init; }

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        DbSet = dbContext.Set<TEntity>();
    }


    public Task<TEntity?> GetByIdAsync(TId id)
    {
        return DbSet.FirstOrDefaultAsync(entity => entity.Id.Equals(id));
    }

    public Task<List<TEntity>> GetAllAsync()
    {
        return DbSet.AsNoTracking().ToListAsync();
    }

    public void Add(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }
}