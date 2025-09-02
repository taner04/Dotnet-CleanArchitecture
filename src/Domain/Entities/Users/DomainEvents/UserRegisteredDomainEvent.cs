using Domain.Abstraction.DomainEvent;

namespace Domain.Entities.Users.DomainEvents;

public readonly record struct UserRegisteredDomainEvent(string FirstName, string LastName, string Email) : IDomainEvent;