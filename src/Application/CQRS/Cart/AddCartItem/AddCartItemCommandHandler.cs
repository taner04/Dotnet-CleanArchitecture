namespace Application.CQRS.Cart.AddCartItem;

public sealed class AddCartItemCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<AddCartItemCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<Result> Handle(AddCartItemCommand command, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.CartRepository.GetCartByUserId(command.UserId);
        if (cart == null)
        {
            cart = Domain.Entities.Carts.Cart.TryCreate(Guid.CreateVersion7(), command.UserId);
            _unitOfWork.CartRepository.Add(cart);
        }

        if (await _unitOfWork.ProductRepository.GetByIdAsync(command.ProductId) == null)
            return Result.Failure(
                ErrorFactory.NotFound("Product not found.")
            );

        cart.AddCartItem(command.ProductId, command.Quantity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}