using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mcare.API.Models
{
    public class RequestRegisterPatient
    {
        public string Email { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NHI { get; set; }
    }
}