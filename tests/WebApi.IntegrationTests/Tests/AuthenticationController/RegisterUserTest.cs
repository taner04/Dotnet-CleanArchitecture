using Application.CQRS.Authentication;
using Domain.Entities.Users;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Shared.WebApi;
using WebApi.IntegrationTests.Common;
using WebApi.IntegrationTests.Factories;
using Email = Domain.Entities.Users.ValueObjects.Email;

namespace WebApi.IntegrationTests.Tests.AuthenticationController;

public class RegisterUserTest(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task RegisterUser_WithValidData_ReturnsSuccess()
    {
        var client = CreateClient();

        var command = new RegisterUser.Command("John", "Doe", "doe@mail.com", "John123!", true);
        var result = await client.PostAsJsonAsync(Routes.Authentication.Register, command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var user = await DbContext.Set<User>()
            .FirstOrDefaultAsync(u => u.Email == Email.From(command.Email), CurrentCancellationToken);

        Assert.NotNull(user);
        Assert.Equal(Email.From(command.Email), user.Email);
        Assert.Equal(command.FirstName, user.FirstName);
        Assert.Equal(command.LastName, user.LastName);
        Assert.Equal(command.WantsEmailNotification, user.WantsEmailNotifications);
        Assert.True(new PasswordService().VerifyPassword(user, command.Password));
    }

    [Fact]
    public async Task RegisterUser_WithInvalidEmail_ReturnsBadRequest()
    {
        var client = CreateClient();

        var command = new RegisterUser.Command("John", "Doe", "doemail.com", "John123!", true);
        var result = await client.PostAsJsonAsync(Routes.Authentication.Register, command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task RegisterUser_WithWeakPassword_ReturnsBadRequest()
    {
        var client = CreateClient();

        var command = new RegisterUser.Command("John", "Doe", "doe@mail.com", "John", true);
        var result = await client.PostAsJsonAsync(Routes.Authentication.Register, command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task RegisterUser_WhenUserAlreadyExists_ReturnsBadRequest()
    {
        DbContext.Set<User>().Add(UserFactory.User());
        await DbContext.SaveChangesAsync(CurrentCancellationToken);

        var client = CreateClient();

        var command = new RegisterUser.Command("John", "Doe", "doe@mail.com", "John123!", true);
        var result = await client.PostAsJsonAsync(Routes.Authentication.Register, command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}