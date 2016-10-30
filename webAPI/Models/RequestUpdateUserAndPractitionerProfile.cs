using mcare.MongoData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mcare.API.Models
{
    public class RequestUpdateUserAndPractitionerProfile
    {
        public User User { get; set; }
        public string UserId {get; set;}

        public PractitionerProfile PractitionerProfile { get; set; }
        public string PractitionerProfileId { get; set; }
    }
}