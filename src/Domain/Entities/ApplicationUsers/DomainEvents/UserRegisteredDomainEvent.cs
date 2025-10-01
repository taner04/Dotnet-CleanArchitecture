using Domain.Common.Abstraction.DomainEvent;

namespace Domain.Entities.ApplicationUsers.DomainEvents;

public record UserRegisteredDomainEvent(ApplicationUser ApplicationUser) : IDomainEvent;