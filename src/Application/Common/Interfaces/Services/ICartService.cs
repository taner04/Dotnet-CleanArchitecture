using Application.Dtos.Cart;
using Application.Dtos.CartItem;
using SharedKernel.Response;
using SharedKernel.Response.Results;

namespace Application.Common.Interfaces.Services;

/// <summary>
/// Service interface for user carts.
/// </summary>
public interface ICartService
{
    /// <summary>
    /// Retrieves the cart for a specific user.
    /// </summary>
    /// <param name="cartByUser">DTO containing user identification.</param>
    /// <returns>Result containing the user's cart data.</returns>
    Task<ResultT<CartDto>> GetCartByUserId(CartByUserDto cartByUser);

    /// <summary>
    /// Adds an item to the user's cart.
    /// </summary>
    /// <param name="addCartItem">DTO containing item details to add.</param>
    /// <returns>Result indicating success or failure.</returns>
    Task<Result> AddItemToCart(AddCartItemDto addCartItem);

    /// <summary>
    /// Removes an item from the user's cart.
    /// </summary>
    /// <param name="removeCartItem">DTO containing item details to remove.</param>
    /// <returns>Result indicating success or failure.</returns>
    Task<Result> RemoveItemFromCart(RemoveCartItemDto removeCartItem);
}