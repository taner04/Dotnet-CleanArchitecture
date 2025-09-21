using System.Net.Http.Json;
using Api.IntegrationTests.Common;
using Application.CQRS.Users;

namespace Api.IntegrationTests.Tests.Users;

public class RegisterUserTest(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task ReturnSuccess()
    {
        var client = GetApiClient();

        var registerCommand = new RegisterUser.Command("John", "Doe", "doe@mail.com", "John123!", true);
        
        var result = await client.PatchAsJsonAsync("auth/register", registerCommand);
        
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}