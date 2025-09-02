using Application.Dtos.Product;

namespace Application.CQRS.Order.CreateOrder;

public readonly record struct CreateOrderCommand(List<ProductOrderCreateDto> Products) : ICommand<Result>;