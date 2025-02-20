using Repositories.Dtos.BackgroundDoctorDto;
using Repositories.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BackgroundDoctorService : IBackgroundDoctorService
    {
        public Task<BackgroundDoctor> CreateBackGroundDoctor(CreateBackgroundDto backgroundDto)
        {
            throw new NotImplementedException();
        }
    }
}
