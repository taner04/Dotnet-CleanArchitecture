using Application.Dtos.User;
using FluentValidation;

namespace Application.Validator.User
{
    public sealed class LoginValidator : AbstractValidator<UserLoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.");
        }
    }
}
