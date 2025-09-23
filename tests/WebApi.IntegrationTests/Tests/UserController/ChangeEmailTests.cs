using Application.CQRS.Users;
using Domain.Entities.Users;
using Domain.Entities.Users.ValueObjects;

namespace Api.IntegrationTests.Tests.UserController;

public class ChangeEmailTests(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task ChangeEmail_WithoutToken_ReturnsUnauthorized()
    {
        var client = CreateClient();

        var command = new ChangeEmail.Command("johndoe@mail.com");
        var response = await client.PostAsJsonAsync(Routes.User.ChangeEmail, command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task ChangeEmail_WithValidToken_ReturnsSuccess()
    {
        var client = await CreateAuthenticatedClientAsync();

        var command = new ChangeEmail.Command("johndoe@mail.com");
        var response = await client.PostAsJsonAsync(Routes.User.ChangeEmail, command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var user = await Repository.SearchByAsync<User>(
            u => u.Email == Email.From(command.NewEmail),
            CurrentCancellationToken);

        Assert.NotNull(user);
        Assert.Equal(command.NewEmail, user.Email.Value);
    }
}