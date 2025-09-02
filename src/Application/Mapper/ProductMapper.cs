using Application.Dtos.Product;
using Domain.Entities.Products;

namespace Application.Mapper;

public static class ProductMapper
{
    public static ProductSnapshotDto ToProductSnapshotDto(this Product product)
    {
        return new ProductSnapshotDto(
            product.Name,
            product.Description,
            product.Price.Value
        );
    }

    public static ProductDto ToProductDto(this Product product)
    {
        return new ProductDto(
            product.Id.Value,
            product.Name,
            product.Description,
            product.Price.Value,
            product.Quantity
        );
    }

    public static ProductCartDto ToProductCartDto(this Product product)
    {
        return new ProductCartDto(
            product.Id.Value,
            product.Name,
            product.Price.Value,
            product.Quantity
        );
    }
}