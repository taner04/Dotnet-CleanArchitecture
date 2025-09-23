using Application.CQRS.Transactions;
using Domain.Entities.Users;

namespace Api.IntegrationTests.Tests.TransactionController;

public class AddTransactionTests(TestingFixture fixture) : TestingBase(fixture)
{
    [Fact]
    public async Task AddTransaction_WithoutToken_ReturnsUnauthorized()
    {
        var client = CreateClient();

        var command = new AddTransaction.Command(100, TransactionType.Expense, "Test Transaction");
        var response = await client.PostAsJsonAsync(Routes.Transaction.Add, command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task AddTransaction_WithValidToken_ReturnsSuccess()
    {
        var client = await CreateAuthenticatedClientAsync();

        var command = new AddTransaction.Command(100, TransactionType.Expense, "Test Transaction");
        var response = await client.PostAsJsonAsync(Routes.Transaction.Add, command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var transactions = await Repository.SearchByAsync<Transaction>(
            t => t.Description == command.Description,
            CurrentCancellationToken);

        Assert.Equal(command.Description, transactions.Description);
        Assert.Equal(command.Amount, transactions.Amount.Value);
        Assert.Equal(command.Type, transactions.Type);
    }
}