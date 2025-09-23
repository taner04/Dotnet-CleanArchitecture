using Domain.Entities.Users.ValueObjects;
using Vogen;

namespace Persistence.Data.Converter;

[EfCoreConverter<Money>]
[EfCoreConverter<Email>]
[EfCoreConverter<Password>]
public sealed partial class ValueObjectConverter;