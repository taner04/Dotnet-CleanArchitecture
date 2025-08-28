using Domain.Abstraction;

namespace Application.Abstraction.Repositories;

public interface IRepository<TEntity, TId> where TEntity : IEntity<TId> where TId : struct
{
    Task<TEntity?> GetByIdAsync(TId id);
    Task<List<TEntity>> GetAllAsync();
    void Add(TEntity entity);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
}