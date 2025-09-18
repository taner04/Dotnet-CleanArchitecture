using Domain.Abstraction.DomainEvent;

namespace Domain.Entities.Users.DomainEvents;

public record UserLoggedInDomainEvent(User User) : IDomainEvent;