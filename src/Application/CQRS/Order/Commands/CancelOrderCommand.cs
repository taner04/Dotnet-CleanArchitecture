using Application.Validator;

namespace Application.CQRS.Order.Commands;

public readonly record struct CancelOrderCommand(Guid OrderId) : ICommand<Result>;

public sealed class CancelOrderCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    : ICommandHandler<CancelOrderCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    private readonly ICurrentUserService _currentUserService =
        currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));

    public async ValueTask<Result> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _unitOfWork.OrderRepository.GetByIdAsync(OrderId.From(command.OrderId));
        if (order is null)
        {
            return ErrorFactory.NotFound($"Order with ID {command.OrderId} not found.");
        }

        if (order.UserId != _currentUserService.GetUserId())
        {
            return ErrorFactory.Unauthorized("You cannot cancel an order that does not belong to you.");
        }

        order.Cancel();

        _unitOfWork.OrderRepository.Delete(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

public sealed class CancelOrderValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderValidator()
    {
        RuleFor(x => x.OrderId).IsId()
            .WithMessage("ID needs to be a valid Guid");
    }
}