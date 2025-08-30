using Application.Abstraction.Utils;
using SharedKernel.Enums;

namespace Application.CQRS.User.DeleteUser;

public sealed class DeleteUserCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async ValueTask<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(command.UserId);
        if (user is null)
            return Result.Failure(
                ErrorFactory.NotFound($"User with ID {command.UserId} not found.")
            );

        var orders = await _unitOfWork.OrderRepository.OrdersByUserAsync(command.UserId);
        if (orders.Any(o => o.Status == OrderStatus.Pending))
            return Result.Failure(
                ErrorFactory.Conflict("User cannot be deleted while having pending orders.")
            );
        
        _unitOfWork.OrderRepository.DeleteRange(orders);   
        
        var cart = await _unitOfWork.CartRepository.GetCartByUserId(command.UserId);
        if (cart != null)
        {
            _unitOfWork.CartRepository.Delete(cart);   
        }
        
        _unitOfWork.UserRepository.Delete(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}