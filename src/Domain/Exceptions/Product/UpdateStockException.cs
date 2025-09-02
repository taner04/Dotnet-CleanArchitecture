using Domain.Entities.Base;

namespace Domain.Exceptions;

public class UpdateStockException(string message) : DomainException(message);