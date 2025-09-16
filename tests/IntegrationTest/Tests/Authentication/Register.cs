using System.Net;
using System.Net.Http.Json;
using Application.CQRS.Users;

namespace IntegrationTest.Tests.Authentication;

public class Register(ApiFixture apiFixture) : TestBase
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
}