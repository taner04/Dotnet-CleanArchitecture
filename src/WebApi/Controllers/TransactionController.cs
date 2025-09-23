using Application.CQRS.Transactions;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;

[Authorize]
[Route("transactions")]
public class TransactionController(IMediator mediator) : ControllerBase
{
   [HttpGet("get-all")] 
   public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
       => MapResult(await mediator.Send(new GetTransactions.Query(), cancellationToken));
   
   [HttpPost("add")]
   public async Task<IActionResult> Add([FromBody] AddTransaction.Command command, CancellationToken cancellationToken)
       => MapResult(await mediator.Send(command, cancellationToken));
}