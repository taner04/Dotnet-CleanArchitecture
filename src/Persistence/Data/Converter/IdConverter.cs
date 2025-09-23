using Domain.Entities.Users;
using Vogen;

namespace Persistence.Data.Converter;

[EfCoreConverter<UserId>]
[EfCoreConverter<AccountId>]
[EfCoreConverter<TransactionId>]
public sealed partial class IdConverter;