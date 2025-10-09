using Domain.Entities.Users;

namespace Domain.UnitTests.Tests;

public class TransactionTests
{
    [Fact]
    public void TryCreate_WithValidParameters_ReturnsTransaction()
    {
        var amount = 100.0m;
        var type = TransactionType.Income;
        var description = "Salary";

        var transaction = Transaction.TryCreate(amount, type, description);

        Assert.NotNull(transaction);
        Assert.Equal(amount, transaction.Amount);
        Assert.Equal(type, transaction.Type);
        Assert.Equal(description, transaction.Description);
        Assert.True(transaction.Date <= DateTime.UtcNow);
    }

    [Fact]
    public void TryCreate_WithEmptyDescription_ThrowsDomainException()
    {
        var amount = 100.0m;
        var type = TransactionType.Expense;

        Assert.Throws<DomainException>(() => Transaction.TryCreate(amount, type, ""));
    }
}