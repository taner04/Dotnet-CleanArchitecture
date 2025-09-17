using System.Net;
using System.Net.Http.Json;
using Api.IntegrationTests.Common;
using Application.CQRS.Users;

namespace Api.IntegrationTests.Tests.Authentication;

public class Register(ApiFixture apiFixture) : TestBase(apiFixture)
{
    [Fact]
    public async Task NewUser_ReturnsOk()
    {
        var httpClient = apiFixture.ApiClient;

        var registerCommand = new RegisterUser.Command
        (
            "John",
            "Doe",
            "doe@mail.com",
            "John123!",
            true
        );
        
        var response = await httpClient.PostAsJsonAsync("auth/register", registerCommand);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ExistingUser_ReturnsConflict()
    {
        var httpClient = apiFixture.ApiClient;

        var registerCommand = new RegisterUser.Command
        (
            "John",
            "Doe",
            "doe@mail.com",
            "John123!",
            true
        );
        
        _ = await httpClient.PostAsJsonAsync("auth/register", registerCommand);
        
        var response = await httpClient.PostAsJsonAsync("auth/register", registerCommand);
        
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }
}