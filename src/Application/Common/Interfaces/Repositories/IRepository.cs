using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        DbContext DbContext { get; init; }

        Task<List<TEntity>> GetAllAsync();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
