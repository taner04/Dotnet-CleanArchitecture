using Vogen;
using AccountId = Domain.Entities.Users.AccountId;
using Email = Domain.Entities.Users.ValueObjects.Email;
using Password = Domain.Entities.Users.ValueObjects.Password;
using TransactionId = Domain.Entities.Users.TransactionId;
using UserId = Domain.Entities.Users.UserId;

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