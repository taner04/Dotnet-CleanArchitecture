using Application.CQRS.Authentication;
using Microsoft.AspNetCore.Authorization;
using Shared.WebApi;

namespace WebApi.Controllers;

public class AuthenticationController(IMediator mediator) : ControllerBase
{
    [HttpPost(Routes.Authentication.Register)]
    public async ValueTask<IActionResult> RegisterAsync([FromBody] RegisterUser.Command command,
        CancellationToken cancellationToken)
    {
        return MapResult(await mediator.Send(command, cancellationToken));
    }

    [HttpPost(Routes.Authentication.Login)]
    public async ValueTask<IActionResult> LoginAsync([FromBody] LoginUser.Query query,
        CancellationToken cancellationToken)
    {
        return MapResult(await mediator.Send(query, cancellationToken));
    }

    [Authorize]
    [HttpGet(Routes.Authentication.RefreshToken)]
    public async ValueTask<IActionResult> RefreshTokenAsync(CancellationToken cancellationToken)
    {
        return MapResult(await mediator.Send(new RefreshToken.Command(), cancellationToken));
    }
}