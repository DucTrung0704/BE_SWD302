using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<BackgroundDoctor> CreateBackgroundDoctorAsync(BackgroundDoctor backgroundDoctor);
        BackgroundDoctor UpdateBackgroundDoctorAsync(BackgroundDoctor backgroundDoctor);
        //Task<BackgroundDoctor> GetBackgroundByUserIdAsync(Guid id);
        //Task<List<BackgroundDoctor>> GetAllDoctorAsync();
    }
}
