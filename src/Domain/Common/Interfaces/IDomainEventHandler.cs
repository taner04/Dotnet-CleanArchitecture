namespace Domain.Common.Interfaces
{
    public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
    {
        /// <summary>
        /// Handles the specified domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event to handle.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task HandleAsync(TEvent domainEvent);
    }
}
