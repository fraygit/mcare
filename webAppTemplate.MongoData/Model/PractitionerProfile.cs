﻿using mcare.MongoData.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcare.MongoData.Model
{
    public class PractitionerProfile : MongoEntity
    {
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Ethnicity { get; set; }
        public string RegistrationNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string PractitionerType { get; set; }
        public List<string> Patients { get; set; }
    }
}
