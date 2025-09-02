using Application.Abstraction.Utils;
using Domain.Abstraction.DomainEvent;
using Domain.Entities.Orders.DomainEvents;

namespace Application.DomainEventHandlers.Order;

public sealed class OrderCancelledDomainEventHandler : IDomainEventHandler<OrderCancelledDomainEvent>
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderCancelledDomainEventHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async ValueTask Handle(OrderCancelledDomainEvent notification, CancellationToken cancellationToken)
    {
        foreach (var (productId, quantity) in notification.Products)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (product is null) continue;

            product.IncreaseStock(quantity);
            _unitOfWork.ProductRepository.Update(product);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}