using Application.CQRS.Accounts;
using Microsoft.AspNetCore.Authorization;
using Shared.WebApi;

namespace WebApi.Controllers;

[Authorize]
public class AccountController(IMediator mediator) : ControllerBase
{
    [HttpGet(Routes.Account.GetBalance)]
    public async Task<IActionResult> GetBalanceAsync(CancellationToken cancellationToken)
    {
        return MapResult(await mediator.Send(new GetBalance.Query(), cancellationToken));
    }
}