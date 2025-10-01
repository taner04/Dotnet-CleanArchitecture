using Domain.Entities.ApplicationUsers;
using Domain.Entities.ApplicationUsers.ValueObjects;
using Vogen;
namespace Persistence.Data;

[EfCoreConverter<UserId>]
[EfCoreConverter<AccountId>]
[EfCoreConverter<TransactionId>]
// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class IdConverter;

[EfCoreConverter<Email>]
[EfCoreConverter<Password>]
// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class ValueObjectConverter;