using System.Net.Http.Json;
using Api.IntegrationTests.Common;
using Api.IntegrationTests.Factories;

namespace Api.IntegrationTests.Tests.Users;

public class RefreshToken(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task RefreshToken_WithValidRefreshToken_ReturnsNewAccessToken()
    {
        await Repository.AddAsync(UserFactory.User(), CurrentCancellationToken);
        
        var client = await CreateAuthenticatedClientAsync(UserFactory.Email, UserFactory.Pwd);
        var response = await client.GetAsync(Routes.Auth.RefreshToken, CurrentCancellationToken);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        Assert.False(string.IsNullOrWhiteSpace( await response.Content.ReadAsStringAsync()));
    }
    
    [Fact]
    public async Task RefreshToken_WithInvalidRefreshToken_ReturnsBadRequest()
    {
        var client = CreateClient(); 
        var response = await client.GetAsync(Routes.Auth.RefreshToken, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}