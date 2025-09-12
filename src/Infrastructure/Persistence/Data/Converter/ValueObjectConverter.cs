using Domain.Entities.Users.ValueObjects;
using Vogen;

namespace Infrastructure.Persistence.Data.Converter;

[EfCoreConverter<Money>]
[EfCoreConverter<Email>]
[EfCoreConverter<Password>]
public sealed partial class ValueObjectConverter;