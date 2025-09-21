using System.Net;
using System.Net.Http.Json;
using Api.IntegrationTests.Common;
using Application.CQRS.Users;
using Domain.Entities.Users;
using Domain.Entities.Users.ValueObjects;

namespace Api.IntegrationTests.Tests.Users;

public class RegisterUserTest(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task ReturnSuccess_WhenCreateNewUser()
    {
        var client = GetApiClient();
        var command = new RegisterUser.Command("John", "Doe", "doe@mail.com", "John123!", true);
        var result = await client.PostAsJsonAsync(Routes.Auth.Register, command);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ReturnBadRequest_WhenEmailIsInvalid()
    {
        var client = GetApiClient();
        var command = new RegisterUser.Command("John", "Doe", "doemail.com", "John123!", true);
        var result = await client.PostAsJsonAsync(Routes.Auth.Register, command);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task ReturnBadRequest_WhenPasswordIsWeak()
    {
        var client = GetApiClient();
        var command = new RegisterUser.Command("John", "Doe", "doemail.com", "John", true);
        var result = await client.PostAsJsonAsync(Routes.Auth.Register, command);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task ReturnBadRequest_WhenUserAlreadyExists()
    {
        var user = User.TryCreate("John", "Doe", Email.From("doe@mail.com"), true);
        user.SetPassword(Password.From("John123!"));
        await Repository.AddAsync(user);

        var client = GetApiClient();
        var command = new RegisterUser.Command("John", "Doe", "doe@mail.com", "John123!", true);
        var result = await client.PostAsJsonAsync(Routes.Auth.Register, command);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}