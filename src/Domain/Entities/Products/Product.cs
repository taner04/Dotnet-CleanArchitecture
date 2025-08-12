namespace Domain.Entities.Products
{
    public sealed class Product : AggregateRoot<ProductId>
    {
#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        private Product() { } // for EF Core
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        
        public Product(ProductId id, string name, string description, decimal price, int quanity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quanity;
        }

        public void UpdateDetails(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public void UpdateQuantity(int amount)
        {
            if(Quantity + amount < 0)
            {
                throw new InvalidOperationException("Insufficient stock to update quantity.");
            }

            Quantity += amount;
        }

        public bool HasSufficientStock(int amount) => Quantity >= amount;

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
    }
}
