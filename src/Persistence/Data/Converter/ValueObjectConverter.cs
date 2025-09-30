using Domain.Entities.Users.ValueObjects;
using Vogen;

namespace Persistence.Data.Converter;

[EfCoreConverter<Email>]
[EfCoreConverter<Password>]
public sealed partial class ValueObjectConverter;