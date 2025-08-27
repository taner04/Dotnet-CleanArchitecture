namespace Application.CQRS.User.RegisterUser;

public readonly record struct RegisterUserCommand(string FirstName, string LastName, string Email, string Password)
    : ICommand<Result>;