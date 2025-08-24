using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Entities.Users;

namespace Infrastructure.Persistence.Repository
{
    [ServiceInjection(typeof(IUnitOfWork), ScopeType.AddTransient)]
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private IRepository<User, UserId>? _userRepository;
        private IRepository<Product, ProductId>? _productRepository;
        private IRepository<Order, OrderId>? _orderRepository;
        
        private bool disposedValue;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IRepository<User, UserId> UserRepository
        {
            get
            {
                _userRepository ??= new Repository<User, UserId>(_dbContext);
                return _userRepository;
            }
        }

        public IRepository<Product, ProductId> ProductRepository
        {
            get
            {
                _productRepository ??= new Repository<Product, ProductId>(_dbContext);
                return _productRepository;
            }
        }

        public IRepository<Order, OrderId> OrderRepository
        {
            get
            {
                _orderRepository ??= new Repository<Order, OrderId>(_dbContext);
                return _orderRepository;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
            => await _dbContext.SaveChangesAsync(cancellationToken);

        private void Dispose(bool disposing)
        {
            if (disposedValue) return;
            
            if (disposing)
            {
                _dbContext?.Dispose();
            }

            disposedValue = true;
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
