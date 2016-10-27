using mcare.MongoData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mcare.API.Models
{
    public class ResponseUserAndPractitionerProfile
    {
        public User User { get; set; }
        public PractitionerProfile PactitionerProfile { get; set; }
    }
}