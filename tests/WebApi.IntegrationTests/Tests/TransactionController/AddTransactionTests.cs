using Application.CQRS.Transactions;
using Domain.Entities.ApplicationUsers;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Shared.WebApi;
using WebApi.IntegrationTests.Common;

namespace WebApi.IntegrationTests.Tests.TransactionController;

public class AddTransactionTests(TestingFixture fixture) : TestingBase(fixture)
{
    private readonly AddTransaction.Command _command = new(100, TransactionType.Expense, "Test Transaction");
    
    [Fact]
    public async Task AddTransaction_WithoutToken_ReturnsUnauthorized()
    {
        var client = CreateClient();
        
        var response = await client.PostAsJsonAsync(Routes.Transaction.Add, _command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task AddTransaction_WithValidToken_ReturnsSuccess()
    {
        var client = await CreateAuthenticatedClientAsync();

        var response = await client.PostAsJsonAsync(Routes.Transaction.Add, _command, CurrentCancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var transactions = await DbContext.Set<Transaction>().FirstOrDefaultAsync(
            t => t.Description == _command.Description,
            CurrentCancellationToken);
        
        Assert.NotNull(transactions);
        Assert.Equal(_command.Description, transactions.Description);
        Assert.Equal(_command.Amount, transactions.Amount);
        Assert.Equal(_command.Type, transactions.Type);
    }
}