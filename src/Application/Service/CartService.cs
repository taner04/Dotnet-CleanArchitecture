using Application.Common.Interfaces;
using Application.Common.Interfaces.Infrastructure;
using Application.Common.Interfaces.Services;
using Application.Dtos.Cart;
using Application.Dtos.CartItem;
using Application.Extensions;
using Application.Mapper;
using Domain.Entities.Carts;
using SharedKernel.Attributes;
using SharedKernel.Enums;
using SharedKernel.Response;
using SharedKernel.Response.Errors;
using SharedKernel.Response.Results;

namespace Application.Service;

[ServiceInjection(typeof(ICartService), ScopeType.Scoped)]
public sealed class CartService : ICartService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidatorFactory _validatorFactory;

    public CartService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory)
    {
        _unitOfWork = unitOfWork;
        _validatorFactory = validatorFactory;
    }

    public async Task<ResultT<CartDto>> GetCartByUserId(CartByUserDto cartByUser)
    {
        var validationResult = _validatorFactory.GetResult(cartByUser);
        if (!validationResult.IsValid)
            return ResultT<CartDto>.Failure(
                ErrorFactory.ValidationError(validationResult.ToDictionary())
            );

        var cart = await _unitOfWork.CartRepository.GetCartByUserId(cartByUser.UserId);
        if (cart != null) return ResultT<CartDto>.Success(cart.ToDto());

        cart = Cart.TryCreate(Guid.CreateVersion7(), cartByUser.UserId);
        await _unitOfWork.SaveChangesAsync();

        return ResultT<CartDto>.Success(cart.ToDto());
    }

    public async Task<Result> AddItemToCart(AddCartItemDto addCartItem)
    {
        var validationResult = _validatorFactory.GetResult(addCartItem);
        if (!validationResult.IsValid)
            return Result.Failure(
                ErrorFactory.ValidationError(validationResult.ToDictionary())
            );

        var cart = await _unitOfWork.CartRepository.GetCartByUserId(addCartItem.UserId);
        if (cart == null)
        {
            cart = Cart.TryCreate(Guid.CreateVersion7(), addCartItem.UserId);
            _unitOfWork.CartRepository.Add(cart);
        }

        if (await _unitOfWork.ProductRepository.GetByIdAsync(addCartItem.ProductId) == null)
            return Result.Failure(
                ErrorFactory.NotFound("Product not found.")
            );

        cart.AddCartItem(addCartItem.ProductId, addCartItem.Quantity);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> RemoveItemFromCart(RemoveCartItemDto removeCartItem)
    {
        var validationResult = _validatorFactory.GetResult(removeCartItem);
        if (!validationResult.IsValid)
            return Result.Failure(
                ErrorFactory.ValidationError(validationResult.ToDictionary())
            );

        var cart = await _unitOfWork.CartRepository.GetCartByUserId(removeCartItem.UserId);
        if (cart == null)
            return Result.Failure(
                ErrorFactory.NotFound("Cart not found for the specified user.")
            );

        var result = cart.TryRemoveCartItem(removeCartItem.CartItemId);
        if (!result.IsSuccess) return result;

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}