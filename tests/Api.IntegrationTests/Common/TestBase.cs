namespace Api.IntegrationTests.Common;


[Collection("ApiCollection")]
public abstract class TestBase(ApiFixture apiFixture) : IAsyncLifetime
{
    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    public Task DisposeAsync()
    {
        throw new NotImplementedException();
    }
}