﻿using mcare.MongoData.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcare.MongoData.Model
{
    public class Appointment : MongoEntity
    {
        public string User { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Details { get; set; }
        public string Location { get; set; }
        public List<Attendees> Attendees { get; set; } 
    }
    public class Attendees
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
