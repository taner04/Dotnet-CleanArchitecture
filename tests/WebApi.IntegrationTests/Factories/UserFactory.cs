using Domain.Entities.ApplicationUsers;
using Domain.Entities.ApplicationUsers.ValueObjects;
using Infrastructure.Utils;

namespace WebApi.IntegrationTests.Factories;

public static class UserFactory
{
    public const string Email = "doe@mail.com";
    public const string Pwd = "John123!";

    private static readonly PasswordService PasswordService = new();

    public static ApplicationUser User()
    {
        var user = ApplicationUser.TryCreate("John", "Doe",
            Domain.Entities.ApplicationUsers.ValueObjects.Email.From(Email), true);

        var pwd = Password.From(PasswordService.HashPassword(Pwd));
        user.SetPassword(pwd);

        return user;
    }
}