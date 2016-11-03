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
    public class PatientProfileRepository : EntityService<PatientProfile>, IPatientProfileRepository
    {
        public async Task<PatientProfile> GetByUser(string username)
        {
            var builder = Builders<PatientProfile>.Filter;
            var filter = builder.Eq("Email", username);
            var patientProfile = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            if (patientProfile.Any())
                return patientProfile.FirstOrDefault();
            return null;
        }
    }
}
