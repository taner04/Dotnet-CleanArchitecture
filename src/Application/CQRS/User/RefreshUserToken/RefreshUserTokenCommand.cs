using Application.Dtos.Jwt;

namespace Application.CQRS.User.RefreshUserToken;

public readonly record struct RefreshUserTokenCommand : ICommand<ResultT<RefreshTokenResponse>>;