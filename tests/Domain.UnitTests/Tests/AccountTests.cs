using Domain.Entities.Users;

namespace Domain.UnitTests.Tests;

public class AccountTests
{
    [Fact]
    public void AddTransaction_ShouldUpdateBalance_WhenIncomeTransaction()
    {
        // Arrange
        var userId = UserId.New();
        var account = new Account(userId);
        var transaction = Transaction.TryCreate(
            100,
            TransactionType.Income,
            "Initial deposit"
        );

        // Act
        account.AddTransaction(transaction);

        // Assert
        Assert.Equal(100, account.Balance.Value);
        Assert.Single(account.Transactions);
        Assert.Equal(transaction, account.Transactions[0]);
    }

    [Fact]
    public void AddTransaction_ShouldUpdateBalance_WhenExpenseTransaction()
    {
        // Arrange
        var userId = UserId.New();
        var account = new Account(userId);
        var income = Transaction.TryCreate(
            200,
            TransactionType.Income,
            "Initial deposit"
        );
        var expense = Transaction.TryCreate(
            50,
            TransactionType.Expense,
            "Grocery shopping"
        );

        // Act
        account.AddTransaction(income);
        account.AddTransaction(expense);

        // Assert
        Assert.Equal(150, account.Balance.Value);
        Assert.Equal(2, account.Transactions.Count);
    }
}