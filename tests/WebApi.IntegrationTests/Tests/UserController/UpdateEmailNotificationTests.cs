using System.Net.Http.Json;
using Api.IntegrationTests.Common;
using Api.IntegrationTests.Factories;
using Application.CQRS.Users;
using Domain.Entities.Users.ValueObjects;

namespace Api.IntegrationTests.Tests.UserController;

public class UpdateEmailNotificationTests(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task UpdateEmailNotification_WithoutToken_ReturnsUnauthorized()
    {
        var client = CreateClient();

        var command = new UpdateEmailNotification.Command(false);
        var response = await client.PostAsJsonAsync(Routes.User.UpdateEmailNotification, command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateEmailNotification_WithValidToken_ReturnsSuccess()
    {
        var client = await CreateAuthenticatedClientAsync();
        
        var command = new UpdateEmailNotification.Command(false);
        var response = await client.PostAsJsonAsync(Routes.User.UpdateEmailNotification, command, CurrentCancellationToken);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var user = await Repository.SearchByAsync<Domain.Entities.Users.User>(
            u => u.Email == Email.From(UserFactory.Email),
            CurrentCancellationToken);
        
        Assert.NotNull(user);
        Assert.Equal(command.WantsEmailNotifications, user.WantsEmailNotifications);
    }
}