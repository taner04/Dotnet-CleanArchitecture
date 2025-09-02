using Domain.Abstraction.DomainEvent;
using Domain.ValueObjects.Identifiers;

namespace Domain.Entities.Orders.DomainEvents;

public readonly record struct OrderCreatedDomainEvent(ProductId ProductId, int Quantity) : IDomainEvent;