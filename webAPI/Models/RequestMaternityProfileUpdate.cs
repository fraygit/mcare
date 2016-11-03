using mcare.MongoData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mcare.API.Models
{
    public class RequestMaternityProfileUpdate
    {
        public User User { get; set; }
        public PatientProfile PatientProfile { get; set; }
        public Maternity Maternity { get; set; }
    }
}