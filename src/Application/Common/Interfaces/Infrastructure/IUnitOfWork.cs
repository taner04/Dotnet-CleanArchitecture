using Application.Common.Interfaces.Infrastructure.Repositories;

namespace Application.Common.Interfaces.Infrastructure;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IProductRepository ProductRepository { get; }
    IOrderRepository OrderRepository { get; }
    ICartRepository CartRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}