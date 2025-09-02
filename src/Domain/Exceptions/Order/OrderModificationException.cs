using Domain.Entities.Base;

namespace Domain.Exceptions.Order;

public class OrderModificationException(string message) : DomainException(message);