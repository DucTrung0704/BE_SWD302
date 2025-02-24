using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        private readonly NuringServiceDbContext _context;
        public UserRepository(NuringServiceDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BackgroundDoctor> CreateBackgroundDoctorAsync(BackgroundDoctor backgroundDoctor)
        {
            await _context.BackgroundDoctors.AddAsync(backgroundDoctor);
            return backgroundDoctor;
        }

        //public async Task<List<BackgroundDoctor>> GetAllDoctorAsync()
        //{
        //    return await _context.BackgroundDoctors.ToListAsync();
        //}

        //public async Task<BackgroundDoctor> GetBackgroundByUserIdAsync(Guid id)
        //{
        //    var bg = await _context.BackgroundDoctors.FirstOrDefaultAsync(b => b.userId == id);
        //    if (bg == null)
        //        throw new Exception("Not found this background");
        //    return bg;
        //}

        public BackgroundDoctor UpdateBackgroundDoctorAsync(BackgroundDoctor backgroundDoctor)
        {
            _context.BackgroundDoctors.Update(backgroundDoctor);
            return backgroundDoctor;
        }
    }
}
