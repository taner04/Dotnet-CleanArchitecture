namespace Application.CQRS.User.RefreshUserToken;

public sealed class RefreshUserTokenCommandValidator : AbstractValidator<RefreshUserTokenCommand>
{
    public RefreshUserTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token must not be empty.");

        RuleFor(x => x.AccessToken)
            .NotEmpty()
            .WithMessage("Access token must not be empty.");
    }
}