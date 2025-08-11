namespace Application.Dtos.Product
{
    public readonly record struct ProductDto(Guid Id, string Name, string Description, decimal Price, int Quantity);
}
