using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Entities.Users;

namespace Application.Common.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User, UserId> UserRepository { get; }
        IRepository<Product, ProductId> ProductRepository { get; }
        IRepository<Order, OrderId> OrderRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
