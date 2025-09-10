using System.Windows.Input;
using Application.Abstraction;
using Domain.Entities.Users;
using ErrorOr;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.Application.RegisterUser;

public readonly struct RegisterUserCommand(string UserName, string Email, string Password) : ICommand<ErrorOr<Success>>;

public sealed class RegisterUserCommandHandler(
    IApplicationDbContext applicationDbContext,
    IBudgetDbContext budgetDbContext,
    UserManager<User> userManager) : ICommandHandler<RegisterUserCommand, ErrorOr<Success>>
{
    public async ValueTask<ErrorOr<Success>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}