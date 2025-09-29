using Application.Common;
using Application.Common.Abstraction.Infrastructure;
using Application.Common.Abstraction.Persistence;
using Shared.Errors;

namespace Application.CQRS.Accounts;

public static class GetBalance
{
    public record Query : IQuery<ErrorOr<decimal>>;

    internal sealed class Handler(
        IApplicationDbContext dbContext,
        UserService userService) : IQueryHandler<Query, ErrorOr<decimal>>
    {
        public async ValueTask<ErrorOr<decimal>> Handle(Query query, CancellationToken cancellationToken)
        {
            var user = await userService.GetCurrentUserWithAccountAsync(cancellationToken);

            return user.GetBalance();
        }
    }
}