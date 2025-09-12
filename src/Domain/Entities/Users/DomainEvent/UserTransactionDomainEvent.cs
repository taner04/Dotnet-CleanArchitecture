using Domain.Abstraction.DomainEvent;

namespace Domain.Entities.Users.DomainEvent;

public record UserTransactionDomainEvent(Transaction Transaction) : IDomainEvent;