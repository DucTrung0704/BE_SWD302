using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Dtos.UserDto;
using Services.Dtos.UserDto;
using Services.Interfaces;

namespace Nuring_Service_Platform.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            await _authService.RegisterAsync(userRegisterDto);
            return Ok(new { message = "User registered successfully", user = userRegisterDto });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserLoginDto userLoginDto)
        {
            return Ok(await _authService.LoginAsync(userLoginDto));
        }
    }
}
