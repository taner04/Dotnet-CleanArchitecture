using Application.CQRS.Users;
using Microsoft.AspNetCore.Authorization;
using Shared.WebApi;

namespace WebApi.Controllers;

[Authorize]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet(Routes.ApplicationUser.GetUserData)]
    public async Task<IActionResult> GetUserDataAsync(CancellationToken cancellationToken)
    {
        return MapResult(await mediator.Send(new GetUserData.Query(), cancellationToken));
    }
}