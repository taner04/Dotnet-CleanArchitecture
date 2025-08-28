using Domain.Abstraction;

namespace Domain.Entities.Base;

public abstract class Entity<TId> : IEntity<TId>
    where TId : struct
{
    public TId Id { get; init; }
}