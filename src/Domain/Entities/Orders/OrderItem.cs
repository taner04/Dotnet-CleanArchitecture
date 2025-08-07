using Domain.ValueObjects;

namespace Domain.Entities.Orders
{
    public sealed class OrderItem : Entity<OrderItemId>
    {
        public ProductId ProductId { get; private set; }
        public int Quantity { get; private set; }
        public Money UnitPrice { get; private set; }

        public Money TotalPrice => Money.From(Quantity * UnitPrice.Value);

#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        private OrderItem() { } // for EF
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.

        public OrderItem(OrderItemId id, ProductId productId, int quantity, Money unitPrice)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
