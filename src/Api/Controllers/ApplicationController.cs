using Api.Extensions;
using Application.CQRS.Users;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/auth")]
public class ApplicationController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async ValueTask<IActionResult> Register([FromBody] RegisterUser.Command command, CancellationToken cancellationToken) 
        => MapResult(await mediator.Send(command, cancellationToken));

    [HttpPost("login")]
    public async ValueTask<IActionResult> Login([FromBody] LoginUser.Query query, CancellationToken cancellationToken)
        => MapResult(await mediator.Send(query, cancellationToken));
    
    [Authorize]
    [HttpGet("refresh-token")]
    public async ValueTask<IActionResult> RefreshToken(CancellationToken cancellationToken)
        => MapResult(await mediator.Send(new RefreshToken.Command(), cancellationToken));
}