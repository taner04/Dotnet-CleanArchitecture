using Vogen;

namespace Infrastructure.Persistence.Configuration
{
    [EfCoreConverter<JwtId>]
    [EfCoreConverter<OrderId>]
    [EfCoreConverter<OrderItemId>]
    [EfCoreConverter<ProductId>]
    [EfCoreConverter<UserId>]
    public sealed partial class VogenEfcConverter;
}
