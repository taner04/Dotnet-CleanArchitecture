using Application.Common.Interfaces.Infrastructure;
using Application.Common.Interfaces.Infrastructure.Repositories;
using Infrastructure.Persistence.Repository;

namespace Infrastructure.Persistence;

[ServiceInjection(typeof(IUnitOfWork), ScopeType.Transient)]
public sealed class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    private IUserRepository? _userRepository;
    private IProductRepository? _productRepository;
    private IOrderRepository? _orderRepository;
    private ICartRepository? _cartRepository;

    private bool _disposedValue;

    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_dbContext);
    public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_dbContext);
    public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_dbContext);
    public ICartRepository CartRepository => _cartRepository ??= new CartRepository(_dbContext);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void Dispose(bool disposing)
    {
        if (_disposedValue) return;

        if (disposing) _dbContext.Dispose();

        _disposedValue = true;
    }

    void IDisposable.Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}