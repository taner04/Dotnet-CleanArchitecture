namespace Application.Dtos.Product;

public readonly record struct ProductCartDto(Guid ProductId, string Name, decimal Price, int Quantity);