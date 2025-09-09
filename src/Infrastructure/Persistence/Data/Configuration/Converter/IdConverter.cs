using Domain.Entities.Users;
using Domain.Entities.Users.ValueObjects;
using Vogen;

namespace Infrastructure.Persistence.Data.Configuration.Converter;

[EfCoreConverter<UserId>]
[EfCoreConverter<AccountId>]
[EfCoreConverter<TransactionId>]
public sealed partial class IdConverter;