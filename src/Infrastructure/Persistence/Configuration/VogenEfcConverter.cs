using Domain.ValueObjects;
using Vogen;

namespace Infrastructure.Persistence.Configuration;

[EfCoreConverter<JwtId>]
[EfCoreConverter<OrderId>]
[EfCoreConverter<OrderItemId>]
[EfCoreConverter<ProductId>]
[EfCoreConverter<UserId>]
[EfCoreConverter<CartId>]
[EfCoreConverter<CartItemId>]
[EfCoreConverter<JwtToken>]
[EfCoreConverter<JwtTokenExpiration>]
[EfCoreConverter<Money>]
public sealed partial class VogenEfcConverter;