﻿using Repositories.Dtos.BackgroundDoctorDto;
using Repositories.Entities;
using Repositories.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBackgroundDoctorService
    {
        Task<BackgroundDoctor> CreateBackGroundDoctor(CreateBackgroundDto backgroundDto);
    }
}
