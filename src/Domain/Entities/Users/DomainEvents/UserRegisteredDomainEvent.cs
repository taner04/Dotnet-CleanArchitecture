using Domain.Abstraction.DomainEvent;

namespace Domain.Entities.Users.DomainEvents;

public record UserRegisteredDomainEvent(User User) : IDomainEvent;