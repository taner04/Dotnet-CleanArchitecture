using Application.Dtos.User;

namespace Application.CQRS.User.LoginUser;

public readonly record struct LoginUserQuery(string Email, string Password) : IQuery<ResultT<AuthResponse>>;