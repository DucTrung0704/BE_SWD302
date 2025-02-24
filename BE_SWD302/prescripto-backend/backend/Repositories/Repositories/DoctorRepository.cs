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
    public class DoctorRepository : GenericRepository<BackgroundDoctor>, IDoctorRepository
    {
        private readonly NuringServiceDbContext _context;
        public DoctorRepository(NuringServiceDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BackgroundDoctor> GetBackgroundByUserIdAsync(Guid id)
        {
            var bg = await _context.BackgroundDoctors.FirstOrDefaultAsync(b => b.userId == id);
            if (bg == null)
                throw new Exception("Not found this background");
            return bg;
        }
    }
}
