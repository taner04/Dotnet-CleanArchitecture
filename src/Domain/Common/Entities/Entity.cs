using Domain.Common.Abstraction.Entity;

namespace Domain.Common.Entities;

public abstract class Entity<TId> : Auditable, IEntity<TId>
    where TId : struct
{
    public TId Id { get; init; }
}