using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dtos.BackgroundDoctorDto
{
    public class UpdateBackgroundDoctorDto
    {
        public List<string>? Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public List<string>? Certifications { get; set; }
    }
}
