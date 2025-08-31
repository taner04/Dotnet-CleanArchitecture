using System.Collections.Immutable;
using Domain.Abstraction;
using Domain.Abstraction.DomainEvent;

namespace Domain.Entities.Base;

public abstract class Entity<TId> : Auditable, IEntity<TId>
    where TId : struct
{
    public TId Id { get; init; }
}