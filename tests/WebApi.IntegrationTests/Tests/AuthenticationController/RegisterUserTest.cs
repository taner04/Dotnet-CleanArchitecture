using System.Net.Http.Json;
using Api.IntegrationTests.Common;
using Api.IntegrationTests.Factories;
using Application.CQRS.Authentication;
using Domain.Entities.Users;
using Domain.Entities.Users.ValueObjects;
using Infrastructure.Utils;

namespace Api.IntegrationTests.Tests.Users;

public class RegisterUserTest(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task RegisterUser_WithValidData_ReturnsSuccess()
    {
        var client = CreateClient();

        var command = new RegisterUser.Command("John", "Doe", "doe@mail.com", "John123!", true);
        var result = await client.PostAsJsonAsync(Routes.Auth.Register, command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var user = await Repository.SearchByAsync<User>(
            u => u.Email == Email.From(command.Email),
            CurrentCancellationToken);
        
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
        var result = await client.PostAsJsonAsync(Routes.Auth.Register, command, CurrentCancellationToken);
        
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task RegisterUser_WithWeakPassword_ReturnsBadRequest()
    {
        var client = CreateClient();
        
        var command = new RegisterUser.Command("John", "Doe", "doe@mail.com", "John", true);
        var result = await client.PostAsJsonAsync(Routes.Auth.Register, command, CurrentCancellationToken);
        
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task RegisterUser_WhenUserAlreadyExists_ReturnsBadRequest()
    {
        await Repository.AddAsync(UserFactory.User(), CurrentCancellationToken);
        var client = CreateClient();
        
        var command = new RegisterUser.Command("John", "Doe", "doe@mail.com", "John123!", true);
        var result = await client.PostAsJsonAsync(Routes.Auth.Register, command, CurrentCancellationToken);
        
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}