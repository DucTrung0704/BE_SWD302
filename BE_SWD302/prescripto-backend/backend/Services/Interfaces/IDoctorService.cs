using Repositories.Dtos.BackgroundDoctorDto;
using Repositories.Entities;
using Repositories.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDoctorService
    {
        Task<BackgroundDoctor> CreateBackGroundDoctor(CreateBackgroundDto createBackgroundDto);
        Task<BackgroundDoctor> UpdateBackgroundDoctor(Guid id, UpdateBackgroundDoctorDto updateBackgroundDoctorDto);
        Task<List<BackgroundDoctor>> GetAllBackgroundDoctor();
        Task<BackgroundDoctor> GetBackgroundDoctor(Guid id);
    }
}
