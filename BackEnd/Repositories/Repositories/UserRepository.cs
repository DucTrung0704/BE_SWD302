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
    }
}
