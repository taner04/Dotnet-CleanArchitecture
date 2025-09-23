using System.Net.Http.Json;
using Api.IntegrationTests.Common;
using Api.IntegrationTests.Factories;
using Application.CQRS.Users;

namespace Api.IntegrationTests.Tests.UserController;

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
        
        var userDto = await response.Content.ReadFromJsonAsync<GetUserData.UserDataDto>();
        Assert.NotNull(userDto);
        Assert.Equal(UserFactory.Email, userDto.Email);
    }
}