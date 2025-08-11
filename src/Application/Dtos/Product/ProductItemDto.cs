namespace Application.Dtos.Product
{
    public readonly record struct ProductItemDto(Guid Id, string Name, string Description, decimal Price, int Quantity);
}
