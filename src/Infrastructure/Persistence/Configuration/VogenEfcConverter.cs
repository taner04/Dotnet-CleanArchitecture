using Domain.ValueObjects;
using Vogen;

namespace Infrastructure.Persistence.Configuration;

/// <summary>
/// Registers EF Core value converters for Vogen value objects used in the domain.
/// This partial class enables Entity Framework Core to persist strongly-typed IDs and value objects.
/// </summary>
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