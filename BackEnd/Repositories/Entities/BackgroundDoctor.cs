using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class BackgroundDoctor : IBaseEntity
    {
        public Guid BgDoctorId { get; set; } 
        public Guid userId { get; set; }
        public string? FullName { get; set; } 
        public DateTime DateOfBirth { get; set; } 
        public List<string>? Specialization { get; set; } 
        public string? LicenseNumber { get; set; }
        public string? Education { get; set; } 
        public int YearsOfExperience { get; set; } 
        public List<string>? Certifications { get; set; } 
        public string? Status { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
