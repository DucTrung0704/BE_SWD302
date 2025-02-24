using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NuringServiceDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IDoctorRepository _doctorRepository;
        public UnitOfWork(NuringServiceDbContext context ,IUserRepository userRepository, IDoctorRepository doctorRepository) 
        { 
            _context = context;
            _userRepository = userRepository;
            _doctorRepository = doctorRepository;
        }
        public IUserRepository UserRepository => _userRepository;

        public IDoctorRepository DoctorRepository => _doctorRepository;

        public async Task<int> SaveChangeAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
