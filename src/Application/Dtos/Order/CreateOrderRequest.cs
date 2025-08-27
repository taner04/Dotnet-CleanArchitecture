using Application.Dtos.Product;

namespace Application.Dtos.Order;

public readonly record struct CreateOrderRequest(Guid UserId, List<ProductOrderCreateDto> Products);