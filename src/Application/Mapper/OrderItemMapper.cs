using Application.Dtos.OrderItem;
using Application.Dtos.Product;

namespace Application.Mapper
{
    public static class OrderItemMapper
    {
        public static OrderItemByOrderDto ToOrderItemByOrderDto(OrderItem orderItem)
        {
            return new(
                orderItem.Quantity,
                orderItem.Amount,
                ProductMapper.ToProductByOrderDto(orderItem.Product)
            );
        }

        public static OrderItem ToOrderItem(ProductCreateOrderDto product)
        {
            return new OrderItem(
                product.Quantity,
                product.Quantity * product.Price
            )
            {
                ProductId = ProductId.From(product.ProductId),
            };
        }
    }
}
