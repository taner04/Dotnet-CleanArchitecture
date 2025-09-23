using Application.CQRS.Accounts;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;

[Authorize]
[Route("accounts")]
public class AccountController(IMediator mediator) : ControllerBase
{
    [HttpGet("get-balance")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return MapResult(await mediator.Send(new GetBalance.Query(), cancellationToken));
    }
}