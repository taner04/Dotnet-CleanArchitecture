using Vogen;

namespace Infrastructure.Persistence.Data.Converter;

[EfCoreConverter<UserId>]
[EfCoreConverter<AccountId>]
[EfCoreConverter<TransactionId>]
public sealed partial class IdConverter;