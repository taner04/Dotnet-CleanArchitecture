namespace Domain.Entities.Products
{
    public sealed class Product : AggregateRoot<ProductId>
    {
        private string _name;
        private string _description;
        private decimal _price;
        private int _quantity;

#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        private Product() { } // for EF Core
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
        
        public Product(ProductId id, string name, string description, decimal price, int quanity)
        {
            Id = id;
            _name = name;
            _description = description;
            _price = price;
            _quantity = quanity;
        }

        public string Name => _name;
        public string Description => _description;
        public decimal Price => _price;
        public int Quantity => _quantity;

        public void UpdateDetails(string name, string description, decimal price)
        {
            _name = name;
            _description = description;
            _price = price;
        }

        public void UpdateQuantity(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative.", nameof(amount));
            }

            if(_quantity + amount < 0)
            {
                throw new InvalidOperationException("Insufficient stock to update quantity.");
            }

            _quantity += amount;
        }

        public bool HasSufficientStock(int quantity) => Quantity >= quantity;
    }

}
