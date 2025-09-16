using Application.CQRS.Accounts;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[Authorize]
[Route("accounts")]
public class AccountController(IMediator mediator) : ControllerBase
{
    [HttpGet("get-balance")] 
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => MapResult(await mediator.Send(new GetBalance.Query(), cancellationToken));
}