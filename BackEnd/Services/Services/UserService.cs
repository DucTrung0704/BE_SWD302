using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Nuring_Service_Platform.Services;
using Repositories.Constants;
using Repositories.Dtos.BackgroundDoctorDto;
using Repositories.Entities;
using Repositories.Interfaces;
using Services.Interfaces;


namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsService _claimsService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(IUnitOfWork unitOfWork, IClaimsService claimsService, IMapper mapper, UserManager<ApplicationUser> userManager) 
        {
            _unitOfWork = unitOfWork;
            _claimsService = claimsService;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<BackgroundDoctor> CreateBackgroundDoctor(CreateBackgroundDto createBackgroundDto)
        {
            var userId = _claimsService.CurrentUserId.ToString();
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("User is not authenticated or claims are invalid.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ArgumentException($"{userId} does not exist");

            var background = _mapper.Map<BackgroundDoctor>(createBackgroundDto);
            background.userId = Guid.Parse(userId);
            background.FullName = user.LastName + " " + user.FirstName;
            background.Status = DoctorStatus.ACTIVATE;
            background.CreatedDate = DateTime.Now.Date;

            await _unitOfWork.UserRepository.CreateBackgroundDoctorAsync(background);
            await _unitOfWork.SaveChangeAsync();
            return background;
        }
    }
}
