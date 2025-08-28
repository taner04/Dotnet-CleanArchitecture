using Application.Abstraction.Utils;
using Application.DomainEvents.Order.Event;

namespace Application.CQRS.Order.CreateOrder;

public sealed class CreateOrderCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateOrderCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<Result> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(command.UserId);
        if (user is null)
            return Result.Failure(
                ErrorFactory.NotFound($"User with ID {command.UserId} not found.")
            );

        var order = Domain.Entities.Orders.Order.TryCreate(Guid.CreateVersion7(), command.UserId);
        foreach (var (productId, quantity) in command.Products)
        {
            var productEntity = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (productEntity is null)
                return Result.Failure(
                    ErrorFactory.NotFound($"Product with ID {productId} not found.")
                );

            var result = productEntity.TryReduceStock(quantity);
            if (!result.IsSuccess) return result;

            order.AddOrderItem(productId, quantity, productEntity.Price);
        }

        order.AddDomainEvent(new OrderConfirmationDomainEvent(command.UserId, order));
        _unitOfWork.OrderRepository.Add(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}