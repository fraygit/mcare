﻿using mcare.MongoData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mcare.API.Models
{
    public class ResponseLogin
    {
        public User UserDetails { get; set; }
        public UserToken UserToken { get; set; }
        public bool HasLogon { get; set; }
        public string UserType { get; set; }
    }
}