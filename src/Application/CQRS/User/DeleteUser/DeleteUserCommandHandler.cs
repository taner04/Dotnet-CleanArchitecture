using Application.Abstraction.Utils;
using Domain.ValueObjects.Identifiers;
using SharedKernel.Enums;

namespace Application.CQRS.User.DeleteUser;

public sealed class DeleteUserCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<DeleteUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly ICurrentUserService _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    
    public async ValueTask<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        if (user is null)
        {
            ErrorFactory.NotFound($"User with ID {userId.Value} not found.");
        }

        var orders = await _unitOfWork.OrderRepository.OrdersByUserAsync(userId);
        if (orders.Any(o => o.Status == OrderStatus.Pending))
        {
            ErrorFactory.Conflict("User cannot be deleted while having pending orders.");
        }
        
        _unitOfWork.OrderRepository.DeleteRange(orders);   
        
        var cart = await _unitOfWork.CartRepository.GetCartByUserId(userId);
        if (cart != null)
        {
            _unitOfWork.CartRepository.Delete(cart);   
        }
        
        _unitOfWork.UserRepository.Delete(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}