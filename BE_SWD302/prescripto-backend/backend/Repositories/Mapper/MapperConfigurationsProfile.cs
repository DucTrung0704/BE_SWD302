using AutoMapper;
using Repositories.Dtos.BackgroundDoctorDto;
using Repositories.Entities;
using Services.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Mapper
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile() 
        {
            CreateMap<UserRegisterDto, ApplicationUser>();
            CreateMap<CreateBackgroundDto, BackgroundDoctor>();
        }
    }
}
