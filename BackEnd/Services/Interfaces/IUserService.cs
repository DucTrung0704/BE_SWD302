﻿using Repositories.Dtos.BackgroundDoctorDto;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<BackgroundDoctor> CreateBackgroundDoctor(CreateBackgroundDto createBackgroundDto);
    }
}
