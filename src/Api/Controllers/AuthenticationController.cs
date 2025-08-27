using Application.Common.Interfaces.Infrastructure;
using Application.Dtos.User;
using Domain.Common.Interfaces.DomainEvent;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest user)
    {
        var result = await _authenticationService.LoginAsync(user);
        return MapResponse(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest user)
    {
        var result = await _authenticationService.RegisterAsync(user);
        return MapResponse(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenDto userByIdDto)
    {
        var result = await _authenticationService.RefreshTokenAsync(userByIdDto);
        return MapResponse(result);
    }
}