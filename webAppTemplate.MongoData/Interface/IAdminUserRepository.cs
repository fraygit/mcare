﻿using mcare.MongoData.Model;
using mcare.MongoData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcare.MongoData.Interface
{
    public interface IAdminUserRepository : IEntityService<AdminUser>
    {
        Task<AdminUser> Get(string adminUsername);
    }
}
