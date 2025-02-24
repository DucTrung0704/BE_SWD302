using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Nuring_Service_Platform.Services;
using Repositories.Constants;
using Repositories.Dtos.BackgroundDoctorDto;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.Migrations;
using Services.Interfaces;


namespace Services.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsService _claimsService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public DoctorService(IUnitOfWork unitOfWork, IClaimsService claimsService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<BackgroundDoctor> CreateBackGroundDoctor(CreateBackgroundDto createBackgroundDto)
        {
            var userId = _claimsService.CurrentUserId.ToString();
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("User is not authenticated or claims are invalid.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ArgumentException($"{userId} does not exist");

            var existBackground = await _unitOfWork.DoctorRepository.GetBackgroundByUserIdAsync(Guid.Parse(userId));
            if (existBackground != null)
                throw new ArgumentException($"A background doctor with userId {userId} already exists.");

            var background = _mapper.Map<BackgroundDoctor>(createBackgroundDto);
            background.userId = Guid.Parse(userId);
            background.FullName = user.LastName + " " + user.FirstName;
            background.Status = DoctorStatus.ACTIVATE;
            background.CreatedDate = DateTime.Now.Date;

            await _unitOfWork.DoctorRepository.AddAsync(background);
            await _unitOfWork.SaveChangeAsync();
            return background;
        }

        public async Task<List<BackgroundDoctor>> GetAllBackgroundDoctor()
        {
            return await _unitOfWork.DoctorRepository.GetAllAsync();
        }

        public async Task<BackgroundDoctor> GetBackgroundDoctor(Guid id)
        {
            return await _unitOfWork.DoctorRepository.GetBackgroundByUserIdAsync(id);
             
        }

        public async Task<BackgroundDoctor> UpdateBackgroundDoctor(Guid id, UpdateBackgroundDoctorDto updateBackgroundDoctorDto)
        {
            var backGround = await _unitOfWork.DoctorRepository.GetBackgroundByUserIdAsync(id);
            if (backGround == null)
                throw new Exception("Not found this background doctor");

            backGround.Specialization = updateBackgroundDoctorDto.Specialization;
            backGround.YearsOfExperience = updateBackgroundDoctorDto.YearsOfExperience;
            backGround.Certifications = updateBackgroundDoctorDto.Certifications;
            backGround.UpdatedDate = DateTime.Now.Date;

            _unitOfWork.DoctorRepository.Update(backGround);
            await _unitOfWork.SaveChangeAsync();
            return backGround;
        }
    }
}
