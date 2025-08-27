namespace Application.Dtos.Product;

public readonly record struct ProductOrderCreateDto(Guid ProductId, int Quantity);