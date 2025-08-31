using Domain.Abstraction.DomainEvent;

namespace Domain.Abstraction;

public interface IEntity<TId> where TId : struct
{
    TId Id { get; init; }
}