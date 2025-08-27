using Domain.Entities.Carts;

namespace Application.Common.Interfaces.Infrastructure.Repositories;

/// <summary>
/// Repository interface for managing <see cref="Cart"/> entities.
/// </summary>
public interface ICartRepository : IRepository<Cart, CartId>
{
    /// <summary>
    /// Retrieves the cart associated with the specified user ID.
    /// </summary>
    /// <param name="userId">The ID of the user whose cart is to be retrieved.</param>
    /// <returns>The <see cref="Cart"/> for the user, or null if not found.</returns>
    Task<Cart?> GetCartByUserId(UserId userId);
}