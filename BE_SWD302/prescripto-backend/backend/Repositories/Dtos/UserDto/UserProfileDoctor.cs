using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dtos.UserDto
{
    public class UserProfileDoctor
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool Gender { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Email { get; set; }
        public string? Specialization { get; set; }
        public int ExperienceYear { get; set; }
    }
}
