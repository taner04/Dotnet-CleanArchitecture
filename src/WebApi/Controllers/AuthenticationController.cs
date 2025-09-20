using WebApi.Extensions;
using Application.CQRS.Users;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;

[Route("auth")]
public class AuthenticationController(IMediator mediator) : ControllerBase
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