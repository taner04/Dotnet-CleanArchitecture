using Application.Common;
using Application.Common.Abstraction.Persistence;

namespace Application.CQRS.Users;

public static class GetUserData
{
    public record Query : IQuery<ErrorOr<UserDataDto>>;

    internal sealed class Handler(
        IApplicationDbContext dbContext,
        UserService userService) : IQueryHandler<Query, ErrorOr<UserDataDto>>
    {
        public async ValueTask<ErrorOr<UserDataDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            var user = await userService.GetCurrentUserAsync(cancellationToken);

            return new UserDataDto(user.FirstName, user.LastName, user.Email.Value);
        }
    }

    public record UserDataDto(string FirstName, string LastName, string Email);
}