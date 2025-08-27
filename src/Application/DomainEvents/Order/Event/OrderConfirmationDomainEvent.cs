using Domain.Common.Interfaces.DomainEvent;

namespace Application.DomainEvents.Order.Event;

public readonly record struct OrderConfirmationDomainEvent(UserId UserId, Domain.Entities.Orders.Order Order)
    : IDomainEvent;