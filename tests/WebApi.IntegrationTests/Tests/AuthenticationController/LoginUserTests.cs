using Application.CQRS.Authentication;
using Domain.Entities.ApplicationUsers;
using Shared.WebApi;
using WebApi.IntegrationTests.Common;
using WebApi.IntegrationTests.Factories;

namespace WebApi.IntegrationTests.Tests.AuthenticationController;

public class LoginUserTests(TestingFixture fixture) : TestingBase(fixture)
{
    private const string InvalidEmail = "doemail.com";
    
    [Fact]
    public async Task Login_WithInvalidEmail_ReturnsBadRequest()
    {
        var query = new LoginUser.Query(InvalidEmail, UserFactory.Pwd);
        var client = CreateClient();

        var response = await client.PostAsJsonAsync(Routes.Authentication.Login, query, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsBadRequest()
    {
        DbContext.Set<ApplicationUser>().Add(UserFactory.User());
        await DbContext.SaveChangesAsync(CurrentCancellationToken);

        var client = CreateClient();

        var invalidEmailQuery = new LoginUser.Query(InvalidEmail, UserFactory.Pwd);
        var invalidEmailResponse =
            await client.PostAsJsonAsync(Routes.Authentication.Login, invalidEmailQuery, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, invalidEmailResponse.StatusCode);

        var invalidPasswordQuery = new LoginUser.Query(UserFactory.Email, "WrongPassword123!");
        var invalidPasswordResponse =
            await client.PostAsJsonAsync(Routes.Authentication.Login, invalidPasswordQuery, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.BadRequest, invalidPasswordResponse.StatusCode);
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsSuccess()
    {
        DbContext.Set<ApplicationUser>().Add(UserFactory.User());
        await DbContext.SaveChangesAsync(CurrentCancellationToken);
        
        var client = CreateClient();

        var query = new LoginUser.Query(UserFactory.Email, UserFactory.Pwd);
        var response = await client.PostAsJsonAsync(Routes.Authentication.Login, query, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Assert.False(string.IsNullOrWhiteSpace(await response.Content.ReadAsStringAsync(CurrentCancellationToken)));
    }
}