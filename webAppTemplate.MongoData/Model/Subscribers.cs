using mcare.MongoData.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcare.MongoData.Model
{
    public class Subscribers : MongoEntity
    {
        public string Email { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
