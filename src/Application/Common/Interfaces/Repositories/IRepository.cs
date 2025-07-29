namespace Application.Common.Interfaces.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        Task<List<TEntity>> GetAllAsync();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
