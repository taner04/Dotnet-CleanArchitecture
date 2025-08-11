namespace Application.Dtos.Jwt
{
    public readonly record struct RefreshTokenResponse(string Token, string RefreshToken);
}
