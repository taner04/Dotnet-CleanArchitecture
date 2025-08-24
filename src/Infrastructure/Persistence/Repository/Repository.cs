using System.Linq.Expressions;
using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public sealed class Repository<TEntity, TId> : IRepository<TEntity, TId> 
        where TEntity : class, IEntity<TId> 
        where TId : struct
    {
        private DbSet<TEntity> DbSet { get; init; }
        public DbContext DbContext { get; init; }

        public Repository(ApplicationDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public Task<TEntity?> GetByIdAsync(TId id) => DbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        public Task<List<TEntity>> GetAllAsync() => DbSet.AsNoTracking().ToListAsync();

        public void Add(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            DbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            DbSet.Update(entity);
        }
        public void Delete(TEntity entity) 
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            DbSet.Remove(entity);
        }
        public Task<TEntity?> FindEntity(Expression<Func<TEntity, bool>> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
            return DbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public Task<List<TEntity>> FindEntities(Expression<Func<TEntity, bool>> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate, nameof(predicate));
            return DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}
