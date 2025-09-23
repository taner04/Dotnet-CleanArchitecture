namespace Api.IntegrationTests.Tests.AccountController;

public class GetBalanceTests(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task GetBalance_WithoutToken_ReturnsUnauthorized()
    {
        var client = CreateClient();
        var response = await client.GetAsync(Routes.Account.GetBalance, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetBalance_WithValidToken_ReturnsSuccess()
    {
        var client = await CreateAuthenticatedClientAsync();
        var response = await client.GetAsync(Routes.Account.GetBalance, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync(CurrentCancellationToken);
        Assert.False(string.IsNullOrWhiteSpace(content));
        Assert.Equal(0, int.Parse(content));
    }
}