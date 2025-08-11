using Application.Dtos.Product;
using Domain.Entities.Products;

namespace Application.Mapper
{
    public static class ProductMapper
    {
        public static ProductSnapshotDto ToProductOrderDto(this Product product)
        {
            return new ProductSnapshotDto(
                product.Name,
                product.Description,
                product.Price
            );
        }

        public static ProductDto ToProductItemDto(this Product product)
        {
            return new ProductDto(
                product.Id.Value,
                product.Name,
                product.Description,
                product.Price,
                product.Quantity
            );
        }
    }
}
