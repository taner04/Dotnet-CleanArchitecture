using Domain.Entities.ApplicationUsers;
using UserId = Domain.Entities.ApplicationUsers.UserId;

namespace Domain.UnitTests.Tests;

public class AccountTests
{
    [Fact]
    public void AddTransaction_ShouldUpdateBalance_WhenIncomeTransaction()
    {
        var account = new Account(UserId.New());
        var transaction = Transaction.TryCreate(100, TransactionType.Income, "Initial deposit");

        account.AddTransaction(transaction);

        Assert.Equal(100, account.Balance);
        Assert.Single(account.Transactions);
        Assert.Equal(transaction, account.Transactions[0]);
    }

    [Fact]
    public void AddTransaction_ShouldUpdateBalance_WhenExpenseTransaction()
    {
        var account = new Account(UserId.New());
        var income = Transaction.TryCreate(200, TransactionType.Income, "Initial deposit");
        var expense = Transaction.TryCreate(50, TransactionType.Expense, "Grocery shopping");

        account.AddTransaction(income);
        account.AddTransaction(expense);

        Assert.Equal(150, account.Balance);
        Assert.Equal(2, account.Transactions.Count);
    }
}