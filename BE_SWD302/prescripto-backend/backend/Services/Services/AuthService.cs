using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repositories.Constants;
using Repositories.Dtos.AuthDto;
using Repositories.Dtos.UserDto;
using Repositories.Entities;
using Services.Dtos.UserDto;
using Services.Interfaces;
using Services.Utils;


namespace Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenProvider _tokenProvider;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, TokenProvider tokenProvider, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenProvider = tokenProvider;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthenticationResponse> LoginAsync(UserLoginDto userLoginDto)
        {
            if (userLoginDto.Username is null || userLoginDto.Password is null)
            {
                throw new ArgumentNullException(nameof(userLoginDto));
            }

            var user = await _userManager.FindByNameAsync(userLoginDto.Username);
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var loginResult = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, isPersistent: true, lockoutOnFailure: false);

            if (!loginResult.Succeeded)
            {
                throw new Exception("Something wrong");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var authenticationResponse = _tokenProvider.GenerateJwtToken(user, roles);

            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration;

            await _userManager.UpdateAsync(user);

            return authenticationResponse;

        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiration = null;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var validationResult = await ValidateRegistrationAsync(userRegisterDto);
            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.Error);
            }

            ApplicationUser newUser = new ApplicationUser
            {
                UserName = userRegisterDto.Username,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Email = userRegisterDto.Email,
                Gender = userRegisterDto.Gender
            };

            var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password!);
            if (!result.Succeeded)
                throw new Exception("somthing wrong");

            string assignedRole = userRegisterDto.Role?.ToUpper() switch
            {
                "DOCTOR" => UserRole.DOCTOR,
                _ => UserRole.CUSTOMER
            };

            await _userManager.AddToRoleAsync(newUser, assignedRole);
        }

        private async Task<(bool IsValid, string Error)> ValidateRegistrationAsync(UserRegisterDto userRegisterDto)
        {
            var userByEmail = await _userManager.FindByEmailAsync(userRegisterDto.Email!);
            if (userByEmail is not null)
                return (false, "This email is already being used");

            var userByName = await _userManager.FindByNameAsync(userRegisterDto.Username!);
            if (userByName is not null)
                return (false, "This username is already being used");

            var newUser = _mapper.Map<ApplicationUser>(userRegisterDto);
            foreach (var validator in _userManager.PasswordValidators)
            {
                var result = await validator.ValidateAsync(_userManager, newUser, userRegisterDto.Password!);
                if (!result.Succeeded)
                {
                    return (false, string.Join("; ", result.Errors.Select(e => e.Description)));
                }
            }

            return (true, string.Empty);
        }
    }
}
