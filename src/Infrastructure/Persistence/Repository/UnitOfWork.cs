namespace Infrastructure.Persistence.Repository
{
    [ServiceInjection(typeof(IUnitOfWork), ScopeType.AddTransient)]
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private IUserRepository? _userRepository;
        private IProductRepository? _productRepository;
        private IOrderRepository? _orderRepository;
        
        private bool disposedValue;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(_dbContext);
                return _userRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                _productRepository ??= new ProductRepository(_dbContext);
                return _productRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                _orderRepository ??= new OrderRepository(_dbContext);
                return _orderRepository;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
            => await _dbContext.SaveChangesAsync(cancellationToken);

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext?.Dispose();
                }

                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
