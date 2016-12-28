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
    public class AppointmentRepository : EntityService<Appointment>, IAppointmentRepository
    {
        public async Task<List<Appointment>> GetByUser(string username)
        {
            var builder = Builders<Appointment>.Filter;
            var filter = builder.Eq("User", username);
            var appointments = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            return appointments;
            return null;
        }
    }
}
