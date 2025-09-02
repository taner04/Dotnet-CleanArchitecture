using Domain.ValueObjects;
using Vogen;
using CartId = Domain.ValueObjects.Identifiers.CartId;
using CartItemId = Domain.ValueObjects.Identifiers.CartItemId;
using OrderId = Domain.ValueObjects.Identifiers.OrderId;
using OrderItemId = Domain.ValueObjects.Identifiers.OrderItemId;
using ProductId = Domain.ValueObjects.Identifiers.ProductId;
using UserId = Domain.ValueObjects.Identifiers.UserId;

namespace Persistence.Data.Configuration;

[EfCoreConverter<OrderId>]
[EfCoreConverter<OrderItemId>]
[EfCoreConverter<ProductId>]
[EfCoreConverter<UserId>]
[EfCoreConverter<CartId>]
[EfCoreConverter<CartItemId>]
public sealed partial class EfcIdConverter;


[EfCoreConverter<JwtToken>]
[EfCoreConverter<JwtTokenExpiration>]
[EfCoreConverter<Money>]
public sealed partial class EfcValueObjectConverter;