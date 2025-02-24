using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nuring_Service_Platform.Services;
using Repositories.Dtos.BackgroundDoctorDto;
using Services.Interfaces;

namespace Nuring_Service_Platform.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IClaimsService _claimsService;
        public DoctorController(IDoctorService doctorService, IClaimsService claimsService)
        {
            _doctorService = doctorService;
            _claimsService = claimsService;
        }

        [HttpPost("profile")]
        public async Task<IActionResult> CreateBackgroundDoctor(CreateBackgroundDto createBackgroundDto)
        {
            return Ok(await _doctorService.CreateBackGroundDoctor(createBackgroundDto));
        }

        [HttpPut("update/profile")]
        public async Task<IActionResult> UpdateBackgroundDoctor(UpdateBackgroundDoctorDto updateBackgroundDoctorDto)
        {
            var userId = _claimsService.CurrentUserId;
            if (userId == Guid.Empty)
                return BadRequest("userId is not found");

            return Ok(await _doctorService.UpdateBackgroundDoctor(userId, updateBackgroundDoctorDto));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBackgroundDoctor()
        {
            return Ok(await _doctorService.GetAllBackgroundDoctor());
        }

        [HttpGet("myProfile")]
        public async Task<IActionResult> GetBackgroundDoctor()
        {
            var userId = _claimsService.CurrentUserId;
            if (userId == Guid.Empty)
                return BadRequest("userId is not found");

            return Ok(await _doctorService.GetBackgroundDoctor(userId));
        }
    }
}
