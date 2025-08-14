namespace Application.Dtos.Jwt
{
    public readonly record struct RefreshTokenResponse(string AccessToken, string RefreshToken);
}
