using Application.Abstraction;
using Application.Dtos.User;
using Application.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Queries;

public readonly record struct GetUserInfoQuery : IQuery<ResultT<UserInfoResponse>>;

public sealed class GetUserInfoQueryHandler(IApplicationDbContext dbContext, 
    ICurrentUserService currentUserService) : IQueryHandler<GetUserInfoQuery, ResultT<UserInfoResponse>>
{
    public async ValueTask<ResultT<UserInfoResponse>> Handle(GetUserInfoQuery query, CancellationToken cancellationToken)
    {
        var userId = currentUserService.GetUserId();
        
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user is null)
        {
            return ErrorFactory.NotFound($"User with ID {userId.Value} not found.");
        }

        return user.ToUserInfoResponse();
    }
}