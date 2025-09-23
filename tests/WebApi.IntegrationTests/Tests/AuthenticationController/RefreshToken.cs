namespace Api.IntegrationTests.Tests.AuthenticationController;

public class RefreshToken(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task RefreshToken_WithValidRefreshToken_ReturnsNewAccessToken()
    {
        var client = await CreateAuthenticatedClientAsync();
        var response = await client.GetAsync(Routes.Auth.RefreshToken, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        Assert.False(string.IsNullOrWhiteSpace(await response.Content.ReadAsStringAsync(CurrentCancellationToken)));
    }

    [Fact]
    public async Task RefreshToken_WithInvalidRefreshToken_ReturnsBadRequest()
    {
        var client = CreateClient();
        var response = await client.GetAsync(Routes.Auth.RefreshToken, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}