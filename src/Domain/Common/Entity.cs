using Domain.Abstraction.Entity;

namespace Domain.Common;

public abstract class Entity<TId> : IEntity<TId> 
    where TId : struct
{
    public TId Id { get; init; }
}