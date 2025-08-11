using Domain.Common.Interfaces.DomainEvent;
using Domain.Entities;

namespace Domain.DomainEvents.Order
{
    public readonly record struct OrderConfirmationDomainEvent(UserId UserId, Entities.Orders.Order Order) : IDomainEvent;
}
