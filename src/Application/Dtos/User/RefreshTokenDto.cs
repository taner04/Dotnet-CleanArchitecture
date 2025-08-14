namespace Application.Dtos.User
{
    public readonly record struct RefreshTokenDto(string AccessToken, string RefreshToken);
}
