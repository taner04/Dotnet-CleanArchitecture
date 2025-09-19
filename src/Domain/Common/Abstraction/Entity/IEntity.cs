namespace Domain.Common.Abstraction.Entity;

/// <summary>
/// Represents an entity with a strongly-typed identifier.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public interface IEntity<TId>
    where TId : struct
{
    /// <summary>
    /// Gets the unique identifier of the entity.
    /// </summary>
    TId Id { get; init; }
}