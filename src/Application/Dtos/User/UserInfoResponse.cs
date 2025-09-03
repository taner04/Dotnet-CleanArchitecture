namespace Application.Dtos.User;

public record UserInfoResponse(
    string Firstname,
    string Lastname,
    string Email);