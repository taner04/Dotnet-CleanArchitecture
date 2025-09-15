using Api.Extensions;
using ErrorOr;

namespace Api.Controllers;

[ApiController]
public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected IActionResult MapResult<T>(ErrorOr<T> result)
    {
        if (result.IsError)
        {
            return result.Errors.ToActionResult(HttpContext);
        }
        
        return Ok(result.Value);
    }
}