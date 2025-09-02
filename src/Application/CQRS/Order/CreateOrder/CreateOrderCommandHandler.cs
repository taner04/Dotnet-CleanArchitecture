using Application.Abstraction.Utils;
using Application.DomainEvents.Order.Event;
using Domain.ValueObjects.Identifiers;

namespace Application.CQRS.Order.CreateOrder;

public sealed class CreateOrderCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateOrderCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<Result> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var userId = UserId.From(command.UserId);
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        if (user is null)
            return Result.Failure(
                ErrorFactory.NotFound($"User with ID {command.UserId} not found.")
            );

        var order = Domain.Entities.Orders.Order.TryCreate(userId);
        foreach (var product in command.Products)
        {
            var productId = ProductId.From(product.ProductId);
            var productEntity = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (productEntity is null)
                return Result.Failure(
                    ErrorFactory.NotFound($"Product with ID {productId} not found.")
                );

            var result = productEntity.TryReduceStock(product.Quantity);
            if (!result.IsSuccess) return result;

            order.AddOrderItem(productId, product.Quantity, productEntity.Price);
        }

        order.AddDomainEvent(new OrderConfirmationDomainEvent(userId, order));
        _unitOfWork.OrderRepository.Add(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}