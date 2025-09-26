using Application.CQRS.Users;
using Microsoft.AspNetCore.Authorization;
using Shared.WebApi;

namespace WebApi.Controllers;

[Authorize]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost(Routes.User.UpdateEmailNotification)]
    public async Task<IActionResult> UpdateEmailNotificationAsync(UpdateEmailNotification.Command command,
        CancellationToken cancellationToken)
    {
        return MapResult(await mediator.Send(command, cancellationToken));
    }

    [HttpPost(Routes.User.ChangeEmail)]
    public async Task<IActionResult> ChangeEmailAsync(ChangeEmail.Command command, CancellationToken cancellationToken)
    {
        return MapResult(await mediator.Send(command, cancellationToken));
    }

    [HttpGet(Routes.User.GetUserData)]
    public async Task<IActionResult> GetUserDataAsync(CancellationToken cancellationToken)
    {
        return MapResult(await mediator.Send(new GetUserData.Query(), cancellationToken));
    }
}