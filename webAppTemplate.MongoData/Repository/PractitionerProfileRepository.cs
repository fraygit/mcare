using mcare.MongoData.Interface;
using mcare.MongoData.Model;
using mcare.MongoData.Service;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcare.MongoData.Repository
{
    public class PractitionerProfileRepository : EntityService<PractitionerProfile>, IPractitionerProfileRepository
    {
        public async Task<PractitionerProfile> GetByUser(string username)
        {
            var builder = Builders<PractitionerProfile>.Filter;
            var filter = builder.Eq("Email", username);
            var practitionerProfile = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            if (practitionerProfile.Any())
                return practitionerProfile.FirstOrDefault();
            return null;
        }
    }
}
