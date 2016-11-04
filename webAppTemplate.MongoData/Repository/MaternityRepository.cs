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
    public class MaternityRepository : EntityService<Maternity>, IMaternityRepository
    {
        public async Task<Maternity> GetCurrentByUser(string username)
        {
            var builder = Builders<Maternity>.Filter;
            var filter = builder.Eq("Email", username) & builder.Eq("Status", "Active");
            var maternity = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            if (maternity.Any())
                return maternity.FirstOrDefault();
            return null;
        }

        public async Task<Maternity> GetByUser(string username)
        {
            var builder = Builders<Maternity>.Filter;
            var filter = builder.Eq("Email", username);
            var maternity = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            if (maternity.Any())
                return maternity.FirstOrDefault();
            return null;
        }
    }
}
