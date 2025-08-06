namespace Application.Dtos.Product
{
    public readonly record struct ProductCreateOrderDto(int ProductId, int Quantity, int Price);
}
