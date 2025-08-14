using Application.Dtos.User;
using FluentValidation;

namespace Application.Validator.Jwt
{
    public sealed class RefreshTokenValidator : AbstractValidator<RefreshTokenDto>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token must not be empty.");

            RuleFor(x => x.AccessToken)
                .NotEmpty()
                .WithMessage("Access token must not be empty.");
        }
    }
}
