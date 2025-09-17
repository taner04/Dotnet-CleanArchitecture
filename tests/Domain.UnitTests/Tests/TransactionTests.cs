using Domain.Common;
using Domain.Entities.Users;

namespace Domain.UnitTests.Tests;

public class TransactionTests
{
    [Fact]
    public void TryCreate_WithValidParameters_ReturnsTransaction()
    {
        // Arrange
        var amount = 100.0m;
        var type = TransactionType.Income;
        var description = "Salary";

        // Act
        var transaction = Transaction.TryCreate(amount, type, description);

        // Assert
        Assert.NotNull(transaction);
        Assert.Equal(amount, transaction.Amount.Value);
        Assert.Equal(type, transaction.Type);
        Assert.Equal(description, transaction.Description);
        Assert.True(transaction.Date <= DateTime.UtcNow);
    }

    [Fact]
    public void TryCreate_WithEmptyDescription_ThrowsDomainException()
    {
        // Arrange
        var amount = 100.0m;
        var type = TransactionType.Expense;
        var description = "";

        // Act & Assert
        Assert.Throws<DomainException>(() => Transaction.TryCreate(amount, type, description));
    }
}