using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Dtos.BackgroundDoctorDto;
using Services.Interfaces;

namespace Nuring_Service_Platform.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("profile")]
        public async Task<IActionResult> CreateBackgroundDoctor(CreateBackgroundDto createBackgroundDto)
        {
            return Ok(await _userService.CreateBackgroundDoctor(createBackgroundDto));
        }
    }
}
