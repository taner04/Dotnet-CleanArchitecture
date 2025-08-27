using Application.Extensions;

namespace Application.CQRS.User.DeleteUser;

public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.UserId).IsId()
            .WithMessage("ID needs to be a valid Guid");
    }
}