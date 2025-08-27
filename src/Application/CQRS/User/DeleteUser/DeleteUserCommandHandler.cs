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

        _unitOfWork.CartRepository.Delete(await _unitOfWork.CartRepository.GetCartByUserId(command.UserId));
        _unitOfWork.OrderRepository.DeleteRange(await _unitOfWork.OrderRepository.OrdersByUserAsync(command.UserId));
        _unitOfWork.UserRepository.Delete(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}