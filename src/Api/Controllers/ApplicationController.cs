using Application.CQRS.Application.RegisterUser;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public class ApplicationController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async ValueTask<IActionResult> Register([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return result.Match<IActionResult>(
            _ => Ok(),
            BadRequest);
    }
}