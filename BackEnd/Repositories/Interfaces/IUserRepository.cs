﻿using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUserRepository 
    {
        Task<BackgroundDoctor> CreateBackgroundDoctorAsync(BackgroundDoctor backgroundDoctor);
    }
}
