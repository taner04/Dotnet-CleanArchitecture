using System.Net.Http.Json;
using Api.IntegrationTests.Common;
using Application.CQRS.Transactions;

namespace Api.IntegrationTests.Tests.TransactionController;

public class GetTransactionTests(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task GetTransaction_WithoutToken_ReturnsUnauthorized()
    {
        var client = CreateClient();

        var response = await client.GetAsync(Routes.Transaction.GetAll, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetTransaction_WithValidToken_ReturnsSuccess()
    {
        var client = await CreateAuthenticatedClientAsync();

        var response = await client.GetAsync(Routes.Transaction.GetAll, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var transactions = await response.Content.ReadFromJsonAsync<List<GetTransactions.TransactionDto>>();
        Assert.NotNull(transactions);
        Assert.Empty(transactions);
    }
}