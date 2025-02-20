using Repositories.Dtos.AuthDto;
using Repositories.Dtos.UserDto;
using Services.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAuthService
    {
        public Task RegisterAsync(UserRegisterDto userRegisterDto);
        public Task<AuthenticationResponse> LoginAsync(UserLoginDto userLoginDto);
    }
}
