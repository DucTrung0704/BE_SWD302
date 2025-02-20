using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dtos.BackgroundDoctorDto
{
    public class CreateBackgroundDto
    {
        public DateTime DateOfBirth { get; set; }
        public List<string>? Specialization { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Education { get; set; }
        public int YearsOfExperience { get; set; }
        public List<string>? Certifications { get; set; }
    }
}
