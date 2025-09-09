using Domain.Entities.Users;
using Domain.Entities.Users.ValueObjects;
using Vogen;

namespace Infrastructure.Persistence.Data.Configuration.Converter;

[EfCoreConverter<Money>]
[EfCoreConverter<Email>]
[EfCoreConverter<Password>]
public sealed partial class ValueObjectConverter;