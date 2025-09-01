using Domain.Abstraction.DomainEvent;

namespace Domain.Abstraction;

/// <summary>
/// Represents a generic entity with a strongly-typed identifier.
/// </summary>
/// <typeparam name="TId">
/// The type of the entity's identifier. Must be a value type.
/// </typeparam>
public interface IEntity<TId> where TId : struct
{
    /// <summary>
    /// Gets the unique identifier for the entity.
    /// </summary>
    TId Id { get; init; }
}