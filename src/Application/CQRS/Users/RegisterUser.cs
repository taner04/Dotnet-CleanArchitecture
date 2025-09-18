using Application.Abstraction.Infrastructure;
using Domain.Entities.Users.ValueObjects;

namespace Application.CQRS.Users;

public static class RegisterUser
{
    public record Command(string FirstName, string LastName, string Email, string Password, bool WantsEmailNotification) : ICommand<ErrorOr<Success>>;
    
    internal sealed class Handler(
        IBudgetDbContext budgetDbContext, 
        IPasswordService passwordService) : ICommandHandler<Command, ErrorOr<Success>>
    {
        public async ValueTask<ErrorOr<Success>> Handle(Command command, CancellationToken cancellationToken)
        {
            var emailResult = Email.TryFrom(command.Email);
            if (!emailResult.IsSuccess)
            {
                return Error.Validation(description: "Invalid email format");
            }
            
            var mail = emailResult.ValueObject;
            if (await budgetDbContext.Users.FirstOrDefaultAsync(u => u.Email == mail, cancellationToken) != null)
            {
                Error.Conflict(description: "User with this email already exists");
            }
        
            var newUser = User.TryCreate(command.FirstName, command.LastName, mail, command.WantsEmailNotification);
        
            var hashedPassword = passwordService.HashPassword(command.Password);
            newUser.SetPassword(Password.From(hashedPassword));
        
            await budgetDbContext.Users.AddAsync(newUser, cancellationToken);
            await budgetDbContext.SaveChangesAsync(cancellationToken);
        
            return Result.Success;
        }
    }
    
    internal sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Password).NotEmpty().MinimumLength(8);
            RuleFor(u => u.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(u => u.LastName).NotEmpty().MaximumLength(50);
            RuleFor(u => u.WantsEmailNotification).NotNull();
        }
    }
}