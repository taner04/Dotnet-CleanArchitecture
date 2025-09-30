using Application.CQRS.Transactions;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Shared.WebApi;
using WebApi.IntegrationTests.Common;

namespace WebApi.IntegrationTests.Tests.TransactionController;

public class AddTransactionTests(TestingFixture fixture) : TestingBase(fixture)
{
    private AddTransaction.Command Command = new(100, TransactionType.Expense, "Test Transaction");
    
    [Fact]
    public async Task AddTransaction_WithoutToken_ReturnsUnauthorized()
    {
        var client = CreateClient();
        
        var response = await client.PostAsJsonAsync(Routes.Transaction.Add, Command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task AddTransaction_WithValidToken_ReturnsSuccess()
    {
        var client = await CreateAuthenticatedClientAsync();

        var response = await client.PostAsJsonAsync(Routes.Transaction.Add, Command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var transactions = await DbContext.Set<Transaction>().FirstOrDefaultAsync(
            t => t.Description == Command.Description,
            CurrentCancellationToken);

        Assert.Equal(Command.Description, transactions.Description);
        Assert.Equal(Command.Amount, transactions.Amount.Value);
        Assert.Equal(Command.Type, transactions.Type);
    }
}