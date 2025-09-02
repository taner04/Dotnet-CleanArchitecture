using Domain.Entities.Base;

namespace Domain.Exceptions.Product;

public class UpdateStockException(string message) : DomainException(message);