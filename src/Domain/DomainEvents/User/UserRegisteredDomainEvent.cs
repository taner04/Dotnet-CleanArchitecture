using Domain.Common.Interfaces.DomainEvent;
using SharedKernel.Attributes;
using SharedKernel.Enums;

namespace Domain.DomainEvents.User
{
    [ServiceInjection(typeof(IDomainEvent), ScopeType.AddTransient)]
    public readonly record struct UserRegisteredDomainEvent(string FirstName, string LastName, string Email) : IDomainEvent;
}
