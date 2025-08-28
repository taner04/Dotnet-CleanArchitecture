using Domain.Abstraction.DomainEvent;

namespace Application.DomainEvents.User.Event;

public readonly record struct UserRegisteredDomainEvent(string FirstName, string LastName, string Email) : IDomainEvent;