using Application.Dtos.User;
using FluentValidation;

namespace Application.Validator.User;

public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Firstname is required.")
            .MaximumLength(50)
            .WithMessage("Firstname must not exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Lastname is required.")
            .MaximumLength(50)
            .WithMessage("Lastname must not exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.")
            .MaximumLength(256);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(256)
            .WithMessage("Password must not exceed 256 characters.");
    }
}