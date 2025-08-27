using Application.Dtos.Product;

namespace Application.Dtos.OrderItem;

public readonly record struct OrderItemDto(decimal Quantity, decimal UnitPrice, ProductSnapshotDto Product);