using Domain.Common.Abstraction.DomainEvent;

namespace Domain.Entities.ApplicationUsers.DomainEvents;

public record UserLoggedInDomainEvent(ApplicationUser ApplicationUser) : IDomainEvent;