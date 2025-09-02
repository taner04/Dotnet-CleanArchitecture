using Domain.ValueObjects;
using Domain.ValueObjects.Identifiers;
using Vogen;

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
[EfCoreConverter<Password>]
[EfCoreConverter<Email>]
public sealed partial class EfcValueObjectConverter;