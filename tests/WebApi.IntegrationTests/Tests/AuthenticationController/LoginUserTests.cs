using System.Net.Http.Json;
using Api.IntegrationTests.Common;
using Api.IntegrationTests.Factories;
using Application.CQRS.Authentication;

namespace Api.IntegrationTests.Tests.Users;

public class LoginUserTests(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task Login_WithInvalidEmail_ReturnsBadRequest()
    {
        var query = new LoginUser.Query("doemail.com", "Test123!");
        var client = CreateClient();

        var response = await client.PostAsJsonAsync(Routes.Auth.Login, query, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsBadRequest()
    {
        await Repository.AddAsync(UserFactory.User(), CurrentCancellationToken);

        var client = CreateClient();

        var invalidEmailQuery = new LoginUser.Query("doemail.com", UserFactory.Pwd);
        var invalidEmailResponse =
            await client.PostAsJsonAsync(Routes.Auth.Login, invalidEmailQuery, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, invalidEmailResponse.StatusCode);

        var invalidPasswordQuery = new LoginUser.Query(UserFactory.Email, "Test123");
        var invalidPasswordResponse =
            await client.PostAsJsonAsync(Routes.Auth.Login, invalidPasswordQuery, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, invalidPasswordResponse.StatusCode);
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsSuccess()
    {
        await Repository.AddAsync(UserFactory.User(), CurrentCancellationToken);

        var client = CreateClient();

        var query = new LoginUser.Query(UserFactory.Email, UserFactory.Pwd);
        var response = await client.PostAsJsonAsync(Routes.Auth.Login, query, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Assert.False(string.IsNullOrWhiteSpace(await response.Content.ReadAsStringAsync()));
    }
}