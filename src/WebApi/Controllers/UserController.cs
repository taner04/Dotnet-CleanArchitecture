using Application.CQRS.Users;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;

[Authorize]
[Route("users")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost("update-email-notification")]
    public async Task<IActionResult> UpdateEmailNotification(UpdateEmailNotification.Command command,
        CancellationToken cancellationToken)
    {
        return MapResult(await mediator.Send(command, cancellationToken));
    }

    [HttpPost("change-email")]
    public async Task<IActionResult> ChangeEmail(ChangeEmail.Command command, CancellationToken cancellationToken)
    {
        return MapResult(await mediator.Send(command, cancellationToken));
    }

    [HttpGet("get-user-data")]
    public async Task<IActionResult> GetUserData(CancellationToken cancellationToken)
    {
        return MapResult(await mediator.Send(new GetUserData.Query(), cancellationToken));
    }
}