using Domain.Common.Interfaces.DomainEvent;

namespace Application.DomainEvents.Order
{
    public sealed class OrderCreatedDomainEvent : IDomainEvent
    {
        public OrderCreatedDomainEvent(string userMail, string trackingNumber)
        {
            UserMail = userMail;
            TrackingNumber = trackingNumber;
        }

        public string UserMail { get; init; }
        public string TrackingNumber { get; init; }
    }
}
