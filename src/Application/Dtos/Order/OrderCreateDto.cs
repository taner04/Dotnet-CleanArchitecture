using Application.Dtos.Product;
using SharedKernel.Enums;

namespace Application.Dtos.Order
{
    public readonly record struct OrderCreateDto(int UserId, PaymendMethod PaymentMethod, List<ProductCreateOrderDto> Products);
}
