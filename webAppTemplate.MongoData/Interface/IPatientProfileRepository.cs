﻿using mcare.MongoData.Model;
using mcare.MongoData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcare.MongoData.Interface
{
    public interface IPatientProfileRepository : IEntityService<PatientProfile>
    {
        Task<PatientProfile> GetByUser(string username);
    }
}
