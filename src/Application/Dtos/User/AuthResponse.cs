namespace Application.Dtos.User;

public record AuthResponse(
    Guid Id,
    string Firstname,
    string Lastname,
    string Mail,
    string AccessToken,
    string RefreshToken);