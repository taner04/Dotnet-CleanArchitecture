using Domain.Abstraction;

namespace Application.Abstraction.Repositories;

/// <summary>
/// Defines a generic repository interface for managing entities with a strongly-typed identifier.
/// </summary>
/// <typeparam name="TEntity">
/// The type of the entity managed by the repository. Must implement <see cref="IEntity{TId}"/>.
/// </typeparam>
/// <typeparam name="TId">
/// The type of the entity's identifier. Must be a value type.
/// </typeparam>
public interface IRepository<TEntity, TId> where TEntity : IEntity<TId> where TId : struct
{
    /// <summary>
    /// Asynchronously retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, <c>null</c>.
    /// </returns>
    Task<TEntity?> GetByIdAsync(TId id);

    /// <summary>
    /// Asynchronously retrieves all entities.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of all entities.
    /// </returns>
    Task<List<TEntity>> GetAllAsync();

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    void Add(TEntity entity);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Updates a range of entities in the repository.
    /// </summary>
    /// <param name="entities">The entities to update.</param>
    void UpdateRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    void Delete(TEntity entity);

    /// <summary>
    /// Deletes a range of entities from the repository.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    void DeleteRange(IEnumerable<TEntity> entities);
}