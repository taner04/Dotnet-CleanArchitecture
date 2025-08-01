namespace Application.Dtos.Product
{
    public readonly record struct ProductDto(int Id, string Name, string Description, decimal Price, int Stock, string ImageUrl);
}
