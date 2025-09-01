using Domain.Abstraction.DomainEvent;
using UserId = Domain.ValueObjects.Identifiers.UserId;

namespace Application.DomainEvents.Order.Event;

public readonly record struct OrderConfirmationDomainEvent(UserId UserId, Domain.Entities.Orders.Order Order)
    : IDomainEvent;