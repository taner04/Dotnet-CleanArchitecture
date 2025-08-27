using Application.Common.Interfaces.Infrastructure.Repositories;
using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public abstract class Repository<TEntity, TId>(ApplicationDbContext dbContext) : IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : struct
    {
        protected DbSet<TEntity> DbSet => dbContext.Set<TEntity>();

        public Task<TEntity?> GetByIdAsync(TId id) => DbSet.FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        public Task<List<TEntity>> GetAllAsync() => DbSet.AsNoTracking().ToListAsync();
        public void Add(TEntity entity) => DbSet.Add(entity);
        public void Update(TEntity entity) => DbSet.Update(entity);
        public void Delete(TEntity entity) => DbSet.Remove(entity);
    }
}
