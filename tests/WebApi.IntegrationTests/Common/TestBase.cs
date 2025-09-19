namespace Api.IntegrationTests.Common;

public abstract class TestBase : IAsyncLifetime
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