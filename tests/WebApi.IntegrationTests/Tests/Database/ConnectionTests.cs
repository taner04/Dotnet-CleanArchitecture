using Api.IntegrationTests.Common;
using Testcontainers.PostgreSql;

namespace Api.IntegrationTests.Tests.Database;

public class ConnectionTests(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task ConnectionStateReturnsOpen() => Assert.True(CanConnect);
}