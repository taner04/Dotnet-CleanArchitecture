using Application.CQRS.Users;
using Shared.WebApi;
using WebApi.IntegrationTests.Common;
using WebApi.IntegrationTests.Factories;

namespace WebApi.IntegrationTests.Tests.UserController;

public class GetUserDataTests(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task GetUserData_WithoutToken_ReturnsUnauthorized()
    {
        var client = CreateClient();
        var response = await client.GetAsync(Routes.User.GetUserData, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetUserData_WithValidToken_ReturnsSuccess()
    {
        var client = await CreateAuthenticatedClientAsync();
        var response = await client.GetAsync(Routes.User.GetUserData, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var userDto = await response.Content.ReadFromJsonAsync<GetUserData.UserDataDto>(CurrentCancellationToken);
        Assert.NotNull(userDto);
        Assert.Equal(UserFactory.Email, userDto.Email);
    }
}