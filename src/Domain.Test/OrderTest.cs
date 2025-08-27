using Domain.Entities.Orders;
using Domain.Exceptions;

namespace Domain.Test;

public class OrderTest
{
    [Test]
    public void CreateOrder_WithValidParameters_ShouldSucceed()
    {
        // Arrange
        var orderId = Guid.CreateVersion7();
        var userId = Guid.CreateVersion7();

        // Act
        var order = Order.TryCreate(orderId, userId);

        // Assert
        Assert.That(order, Is.Not.Null);
        Assert.That(order.Id, Is.EqualTo(orderId));
        Assert.That(order.UserId, Is.EqualTo(userId));
    }

    [Test]
    public void CreateOrder_WithEmptyOrderId_ShouldThrowInvalidIdException()
    {
        // Arrange
        var emptyOrderId = Guid.Empty;
        var userId = Guid.CreateVersion7();

        // Act & Assert
        var ex = Assert.Throws<InvalidIdException>(() => Order.TryCreate(emptyOrderId, userId));
        Assert.That(ex.Message, Is.EqualTo("ID must not be empty."));
    }

    [Test]
    public void CreateOrder_WithEmptyUserId_ShouldThrowInvalidIdException()
    {
        // Arrange
        var orderId = Guid.CreateVersion7();
        var emptyUserId = Guid.Empty;

        // Act & Assert
        var ex = Assert.Throws<InvalidIdException>(() => Order.TryCreate(orderId, emptyUserId));
        Assert.That(ex.Message, Is.EqualTo("ID must not be empty."));
    }

    [Test]
    public void AddOrderItem_WithValidParameters_ShouldSucceed()
    {
        // Arrange
        var orderId = Guid.CreateVersion7();
        var userId = Guid.CreateVersion7();
        var order = Order.TryCreate(orderId, userId);

        var productId = Guid.CreateVersion7();
        var quantity = 2;
        var unitPrice = ValueObjects.Money.From(50m);

        // Act & Assert
        order.AddOrderItem(productId, quantity, unitPrice);

        Assert.That(order.OrderItems, Has.Count.EqualTo(1));

        var orderItem = order.OrderItems.First();
        Assert.That(orderItem.ProductId, Is.EqualTo(productId));
        Assert.That(orderItem.Quantity, Is.EqualTo(quantity));
        Assert.That(orderItem.UnitPrice, Is.EqualTo(unitPrice));
        Assert.That(order.TotalPrice, Is.EqualTo(quantity * unitPrice.Value));
    }

    [Test]
    public void AddOrderItem_WithInvalidQuantity_ShouldThrowValueBelowMinimumException()
    {
        // Arrange
        var orderId = Guid.CreateVersion7();
        var userId = Guid.CreateVersion7();
        var order = Order.TryCreate(orderId, userId);

        var productId = Guid.CreateVersion7();
        var invalidQuantity = 0; // Invalid quantity

        var unitPrice = ValueObjects.Money.From(50m);

        // Act & Assert
        var ex = Assert.Throws<ValueBelowMinimumException>(() =>
            order.AddOrderItem(productId, invalidQuantity, unitPrice));
        Assert.That(ex.Message, Is.EqualTo("Quantity must be greater than zero."));
    }

    [Test]
    public void AddOrderItem_WithInvalidUnitPrice_ShouldThrowValueBelowMinimumException()
    {
        // Arrange
        var orderId = Guid.CreateVersion7();
        var userId = Guid.CreateVersion7();
        var order = Order.TryCreate(orderId, userId);

        var productId = Guid.CreateVersion7();
        var quantity = 2;
        var invalidUnitPrice = ValueObjects.Money.From(0m); // Invalid unit price

        // Act & Assert
        var ex = Assert.Throws<ValueBelowMinimumException>(() =>
            order.AddOrderItem(productId, quantity, invalidUnitPrice));
        Assert.That(ex.Message, Is.EqualTo("Unit price must be greater than zero."));
    }
}