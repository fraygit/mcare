﻿using mcare.MongoData.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcare.MongoData.Model
{
    public class PatientProfile : MongoEntity
    {
        public string Email { get; set; }
        public string NHI { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        public string Ethnicity { get; set; }
        public string MaritalStatus { get; set; }
        public string Alergy { get; set; }
        public string Medications { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}
