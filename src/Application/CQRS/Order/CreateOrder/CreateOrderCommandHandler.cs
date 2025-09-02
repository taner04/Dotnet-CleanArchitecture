using Application.Abstraction.Utils;
using Domain.Entities.Orders.DomainEvents;
using Domain.ValueObjects.Identifiers;

namespace Application.CQRS.Order.CreateOrder;

public sealed class CreateOrderCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<CreateOrderCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

    public async ValueTask<Result> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        if (user is null)
        {
            return Result.Failure(
                ErrorFactory.NotFound($"User with ID {userId.Value} not found.")
            );
        }

        var order = Domain.Entities.Orders.Order.TryCreate(userId);
        foreach (var product in command.Products)
        {
            var productId = ProductId.From(product.ProductId);
            var productEntity = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (productEntity is null)
            {
                return Result.Failure(
                    ErrorFactory.NotFound($"Product with ID {productId} not found.")
                );
            }

            productEntity.ReduceStock(product.Quantity);
            _unitOfWork.ProductRepository.Update(productEntity);

            order.AddOrderItem(productId, product.Quantity, productEntity.Price);
        }

        _unitOfWork.OrderRepository.Add(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}