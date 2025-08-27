using Application.Dtos.Jwt;

namespace Application.CQRS.User.RefreshUserToken;

public readonly record struct RefreshUserTokenCommand(string AccessToken, string RefreshToken)
    : ICommand<ResultT<RefreshTokenResponse>>;