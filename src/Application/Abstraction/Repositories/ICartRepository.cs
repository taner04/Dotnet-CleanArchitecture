using Domain.Entities.Carts;
using CartId = Domain.ValueObjects.Identifiers.CartId;
using UserId = Domain.ValueObjects.Identifiers.UserId;

namespace Application.Abstraction.Repositories;

/// <summary>
/// Defines the contract for a repository that manages <see cref="Cart"/> entities.
/// </summary>
public interface ICartRepository : IRepository<Cart, CartId>
{
    /// <summary>
    /// Retrieves the cart associated with the specified user identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="Cart"/>
    /// associated with the user, or <c>null</c> if no cart exists for the user.
    /// </returns>
    Task<Cart?> GetCartByUserId(UserId userId);
}