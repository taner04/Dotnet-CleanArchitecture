using Application.CQRS.Transactions;
using Microsoft.AspNetCore.Authorization;
using Shared.WebApi;

namespace WebApi.Controllers;

[Authorize]
public class TransactionController(IMediator mediator) : ControllerBase
{
    [HttpGet(Routes.Transaction.GetAll)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        return MapResult(await mediator.Send(new GetTransactions.Query(), cancellationToken));
    }

    [HttpPost(Routes.Transaction.Add)]
    public async Task<IActionResult> AddAsync([FromBody] AddTransaction.Command command,
        CancellationToken cancellationToken)
    {
        return MapResult(await mediator.Send(command, cancellationToken));
    }
}