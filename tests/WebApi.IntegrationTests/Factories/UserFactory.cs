using Domain.Entities.Users;
using Infrastructure.Utils;
using Password = Domain.Entities.Users.ValueObjects.Password;

namespace WebApi.IntegrationTests.Factories;

public static class UserFactory
{
    public const string Email = "doe@mail.com";
    public const string Pwd = "John123!";

    private static readonly PasswordService PasswordService = new();

    public static User User()
    {
        var user = Domain.Entities.Users.User.TryCreate("John", "Doe",
            Domain.Entities.Users.ValueObjects.Email.From(Email), true);

        var pwd = Password.From(PasswordService.HashPassword(Pwd));
        user.SetPassword(pwd);

        return user;
    }
}