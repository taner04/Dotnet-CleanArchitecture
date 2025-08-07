namespace Application.Dtos.Jwt
{
    public readonly record struct JwtRefreshedTokenDto(string Token, string RefreshToken);
}
