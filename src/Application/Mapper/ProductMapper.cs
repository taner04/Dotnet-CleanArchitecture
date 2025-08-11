using Application.Dtos.Product;
using Domain.Entities.Products;

namespace Application.Mapper
{
    public static class ProductMapper
    {
        public static ProductOrderDto ToProductOrderDto(this Product product)
        {
            return new ProductOrderDto(
                product.Name,
                product.Description,
                product.Price
            );
        }

        public static ProductItemDto ToProductItemDto(this Product product)
        {
            return new ProductItemDto(
                product.Id.Value,
                product.Name,
                product.Description,
                product.Price,
                product.Quantity
            );
        }
    }
}
