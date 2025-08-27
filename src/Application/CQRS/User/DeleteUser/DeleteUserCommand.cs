namespace Application.CQRS.User.DeleteUser;

public readonly record struct DeleteUserCommand(Guid UserId) : ICommand<Result>;