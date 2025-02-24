using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IDoctorRepository DoctorRepository { get; }
        public Task<int> SaveChangeAsync();
    }
}
