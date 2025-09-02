using Domain.Abstraction.DomainEvent;
using Domain.ValueObjects.Identifiers;

namespace Domain.Entities.Orders.DomainEvents;

public readonly record struct OrderCancelledDomainEvent(Dictionary<ProductId, int> Products) : IDomainEvent;