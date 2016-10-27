using mcare.MongoData.Entities.Base;
using System;

namespace mcare.MongoData.Model
{
    public class User : MongoEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }
        public DateTime LastLogon { get; set; }
        public bool HasLogon { get; set; }
    }
}
