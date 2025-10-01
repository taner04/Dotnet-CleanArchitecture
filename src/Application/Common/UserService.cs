using Application.Common.Abstraction.Infrastructure;
using Application.Common.Abstraction.Persistence;
using Domain.Common;
using Domain.Entities.ApplicationUsers;
using Shared.Errors;

namespace Application.Common;

public class UserService(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
{
    public async Task<ApplicationUser> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        var userId = currentUserService.GetUserId();
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        
        return user ?? throw new DomainException(UserErrors.Unauthorized);
    }

    public async Task<ApplicationUser> GetCurrentUserWithAccountAsync(CancellationToken cancellationToken)
    {
        var userId = currentUserService.GetUserId();
        var user = await dbContext.Users
            .Where(u => u.Id == userId)
            .Include(u => u.Account)
            .FirstOrDefaultAsync(cancellationToken);
        
        return user ?? throw new DomainException(UserErrors.Unauthorized);
    }

    public async Task<ApplicationUser> GetCurrentUserWithAccountAndTransactionsAsync(CancellationToken cancellationToken)
    {
        var userId = currentUserService.GetUserId();
        var user = await dbContext.Users
            .Where(u => u.Id == userId)
            .Include(u => u.Account)
            .ThenInclude(a => a.Transactions)
            .FirstOrDefaultAsync(cancellationToken);
        
        return user ?? throw new DomainException(UserErrors.Unauthorized);
    }
}
