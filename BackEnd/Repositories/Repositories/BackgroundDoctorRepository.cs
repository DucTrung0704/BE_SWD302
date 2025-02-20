using Repositories.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class BackgroundDoctorRepository : GenericRepository<BackgroundDoctor>, IBackgroundDoctorRepository
    {
        public BackgroundDoctorRepository(NuringServiceDbContext context) : base(context)
        {
        }
    }
}
