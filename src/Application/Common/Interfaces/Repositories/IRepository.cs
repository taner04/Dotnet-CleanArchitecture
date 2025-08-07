using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : IEntity<TId> where TId : struct
    {
        DbContext DbContext { get; init; }

        Task<TEntity?> GetByIdAsync(TId id);
        Task<List<TEntity>> GetAllAsync();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}