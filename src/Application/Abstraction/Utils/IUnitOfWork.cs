using Application.Abstraction.Repositories;

namespace Application.Abstraction.Utils;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IProductRepository ProductRepository { get; }
    IOrderRepository OrderRepository { get; }
    ICartRepository CartRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}