using Application.CQRS.User.Commands;
using Application.CQRS.User.Queries;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("api/user")]
public sealed class UserController(IMediator mediator) : ControllerBase
{
    [HttpDelete("delete-account")]
    public async Task<IActionResult> DeleteUserAccountAsync(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteUserCommand(), cancellationToken);
        return MapResponse(result);
    }

    [HttpGet("get-info")]
    public async Task<IActionResult> GetUserInfoAsync(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserInfoQuery(), cancellationToken);
        return MapResponse(result);
    }
}