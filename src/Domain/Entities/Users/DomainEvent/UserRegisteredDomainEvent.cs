using Domain.Abstraction.DomainEvent;
using Domain.Entities.Users.ValueObjects;

namespace Domain.Entities.Users.DomainEvent;

public record UserRegisteredDomainEvent(User User) : IDomainEvent;