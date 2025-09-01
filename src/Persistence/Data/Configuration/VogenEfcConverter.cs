using Domain.ValueObjects;
using Vogen;
using CartId = Domain.ValueObjects.Identifiers.CartId;
using CartItemId = Domain.ValueObjects.Identifiers.CartItemId;
using JwtId = Domain.ValueObjects.Identifiers.JwtId;
using OrderId = Domain.ValueObjects.Identifiers.OrderId;
using OrderItemId = Domain.ValueObjects.Identifiers.OrderItemId;
using ProductId = Domain.ValueObjects.Identifiers.ProductId;
using UserId = Domain.ValueObjects.Identifiers.UserId;

namespace Persistence.Data.Configuration;

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