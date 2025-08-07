using Application.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto user)
        {
            var result = await _authenticationService.LoginAsync(user);
            return MapResponse(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterDto user)
        {
            var result = await _authenticationService.RegisterAsync(user);
            return MapResponse(result);
        }

        [HttpPost("refresh-token")]
        public Task<IActionResult> RefreshTokenAsync()
        {
            throw new NotImplementedException("Refresh token functionality is not implemented yet.");
        }
    }
}
