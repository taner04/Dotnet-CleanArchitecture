using System.Linq.Expressions;
using Domain.Common.Interfaces;

namespace Application.Common.Interfaces.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : IEntity<TId> where TId : struct
    {
        Task<TEntity?> GetByIdAsync(TId id);
        Task<List<TEntity>> GetAllAsync();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity?> FindEntity(Expression<Func<TEntity, bool>> predicate);
        Task< List<TEntity>> FindEntities(Expression<Func<TEntity, bool>> predicate);
    }
}