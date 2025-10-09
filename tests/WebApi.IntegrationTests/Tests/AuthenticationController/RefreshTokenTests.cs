using Application.CQRS.Authentication;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Shared.WebApi;
using WebApi.IntegrationTests.Common;
using WebApi.IntegrationTests.Factories;
using Email = Domain.Entities.Users.ValueObjects.Email;

namespace WebApi.IntegrationTests.Tests.AuthenticationController;

public class RefreshTokenTests(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task RefreshToken_WithValidRefreshToken_ReturnsNewAccessToken()
    {
        var client = await CreateAuthenticatedClientAsync();

        var user = await DbContext.Set<User>()
            .FirstOrDefaultAsync(u => u.Email == Email.From(UserFactory.Email), CurrentCancellationToken);

        var command = new RefreshToken.Command(user.RefreshToken);
        var response =
            await client.PutAsJsonAsync(Routes.Authentication.RefreshToken, command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Assert.False(string.IsNullOrWhiteSpace(await response.Content.ReadAsStringAsync(CurrentCancellationToken)));
    }

    [Fact]
    public async Task RefreshToken_WithInvalidRefreshToken_ReturnsBadRequest()
    {
        var client = CreateClient();

        var command = new RefreshToken.Command("");
        var response =
            await client.PutAsJsonAsync(Routes.Authentication.RefreshToken, command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}