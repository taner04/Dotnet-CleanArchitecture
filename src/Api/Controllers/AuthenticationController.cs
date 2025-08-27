using Application.Common.Interfaces.Infrastructure;
using Application.CQRS.User.LoginUser;
using Application.CQRS.User.RefreshUserToken;
using Application.CQRS.User.RegisterUser;
using Application.Dtos.User;
using Domain.Common.Interfaces.DomainEvent;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserQuery query)
    {
        var result = await _mediator.Send(query);
        return MapResponse(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        return MapResponse(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshUserTokenCommand command)
    {
        var result = await _mediator.Send(command);
        return MapResponse(result);
    }
}