namespace Application.Dtos.User;

public record AuthResponse(
    string Firstname,
    string Lastname,
    string Email,
    string AccessToken,
    string RefreshToken);