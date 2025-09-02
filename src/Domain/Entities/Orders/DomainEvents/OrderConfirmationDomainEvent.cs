using Domain.Abstraction.DomainEvent;
using UserId = Domain.ValueObjects.Identifiers.UserId;

namespace Domain.Entities.Orders.DomainEvents;

public readonly record struct OrderConfirmationDomainEvent(UserId UserId, Order Order)
    : IDomainEvent;