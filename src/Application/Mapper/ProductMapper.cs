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
    }
}
