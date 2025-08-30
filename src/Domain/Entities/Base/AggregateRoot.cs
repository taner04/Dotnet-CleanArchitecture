using Domain.Abstraction;
using Domain.Abstraction.DomainEvent;

namespace Domain.Entities.Base;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : struct
{

}