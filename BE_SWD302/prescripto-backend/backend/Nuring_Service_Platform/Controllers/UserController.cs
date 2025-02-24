using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nuring_Service_Platform.Services;
using Repositories.Dtos.BackgroundDoctorDto;
using Repositories.Entities;
using Services.Interfaces;

namespace Nuring_Service_Platform.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IClaimsService _claimsService;
        public UserController(IUserService userService, IClaimsService claimsService)
        {
            _userService = userService;
            _claimsService = claimsService;
        }

        //[HttpPost("profile")]
        //public async Task<IActionResult> CreateBackgroundDoctor(CreateBackgroundDto createBackgroundDto)
        //{
        //    return Ok(await _userService.CreateBackgroundDoctor(createBackgroundDto));
        //}

        //[HttpPut("doctor/profile")]
        //public async Task<IActionResult> UpdateBackgroundDoctor(UpdateBackgroundDoctorDto updateBackgroundDoctorDto)
        //{
        //    var userId = _claimsService.CurrentUserId;
        //    if (userId == Guid.Empty)
        //        return BadRequest("userId is not found");
        //    return Ok(await _userService.UpdateBackgroundDoctor(userId, updateBackgroundDoctorDto));
        //}

        //[HttpGet("doctor/all")]
        //public async Task<IActionResult> GetAllBackgroundDoctor()
        //{
        //    return Ok(await _userService.GetAllBackgroundDoctor());
        //}
    }
}
