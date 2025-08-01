using Application.Dtos.Product;

namespace Application.Mapper
{
    public static class ProductMapper
    {
        public static ProductByOrderDto ToProductByOrderDto(Product product)
        {
            return new(
                product.Name,
                product.Description,
                product.ImageUrl
            );
        }

        public static ProductDto ToProductDto(Product product)
        {
            return new(
                product.Id.Value,
                product.Name,
                product.Description,
                product.Price,
                product.Stock,
                product.ImageUrl
            );
        }
    }
}
