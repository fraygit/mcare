using mcare.MongoData.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcare.MongoData.Model
{
    public class Maternity : MongoEntity
    {
        public string Email { get; set; }
        public DateTime LastPeriod { get; set; }
        public DateTime EDD { get; set; }
        public DateTime ConceptionDate { get; set; }
        public string Status { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}
