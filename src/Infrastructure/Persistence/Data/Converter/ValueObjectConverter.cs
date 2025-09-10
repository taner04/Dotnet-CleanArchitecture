using Domain.Entities.Users.ValueObjects;
using Vogen;

namespace Infrastructure.Persistence.Data.Converter;

[EfCoreConverter<Money>]
public sealed partial class ValueObjectConverter;