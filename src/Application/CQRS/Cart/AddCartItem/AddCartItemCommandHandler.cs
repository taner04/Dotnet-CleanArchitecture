using Application.Abstraction.Utils;
using Domain.ValueObjects.Identifiers;

namespace Application.CQRS.Cart.AddCartItem;

public sealed class AddCartItemCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<AddCartItemCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<Result> Handle(AddCartItemCommand command, CancellationToken cancellationToken)
    {
        var userId = UserId.From(command.UserId);
        var cart = await _unitOfWork.CartRepository.GetCartByUserId(userId);
        if (cart == null)
        {
            cart = Domain.Entities.Carts.Cart.TryCreate(userId);
            _unitOfWork.CartRepository.Add(cart);
        }

        var productId = ProductId.From(command.ProductId);
        if (await _unitOfWork.ProductRepository.GetByIdAsync(productId) == null)
            return Result.Failure(
                ErrorFactory.NotFound("Product not found.")
            );

        cart.AddCartItem(productId, command.Quantity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}