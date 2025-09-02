using Domain.Entities.Base;

namespace Domain.Exceptions.Order;

public class OrderCancelException() : DomainException("Order status is not pending");