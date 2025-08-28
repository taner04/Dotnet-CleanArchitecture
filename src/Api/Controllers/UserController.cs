using Application.CQRS.User.DeleteUser;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[Route("api/user")]
public sealed class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpDelete("delete-account")]
    public async Task<IActionResult> DeleteUserAccountAsync([FromBody] DeleteUserCommand command)
    {
        var result = await _mediator.Send(command);
        return MapResponse(result);
    }
}