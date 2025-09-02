using Application.Abstraction.Utils;
using Domain.ValueObjects.Identifiers;
using SharedKernel.Enums;

namespace Application.CQRS.Order.CancelOrder;

public sealed class CancelOrderCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<CancelOrderCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    
    public async ValueTask<Result> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(OrderId.From(command.OrderId));
        if (order is null)
            return Result.Failure(
                ErrorFactory.NotFound($"Order with ID {command.OrderId} not found.")
            );

        if (order.UserId != _currentUserService.GetUserId())
            return Result.Failure(
                ErrorFactory.Unauthorized("You cannot cancel an order that does not belong to you.")
            );

        if (order.Status != OrderStatus.Pending)
            return Result.Failure(
                ErrorFactory.Conflict("Only pending orders can be cancelled.")
            );

        order.UpdateStatus(OrderStatus.Cancelled);
        foreach (var orderItem in order.OrderItems)
        {
            var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(orderItem.ProductId);
            if (existingProduct == null) continue;

            var result = existingProduct.TryIncreaseStock(orderItem.Quantity);
            if (!result.IsSuccess) return result;
        }

        _unitOfWork.OrderRepository.Delete(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}