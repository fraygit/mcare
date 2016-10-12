using MongoDB.Driver;
using mcare.MongoData.Interface;
using mcare.MongoData.Model;
using mcare.MongoData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mcare.MongoData.Repository
{
    public class AdminUserRepository : EntityService<AdminUser>, IAdminUserRepository
    {
        public async Task<AdminUser> Get(string adminUsername)
        {
            var builder = Builders<AdminUser>.Filter;
            var filter = builder.Eq("Email", adminUsername);
            var users = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            if (users.Any())
                return users.FirstOrDefault();
            return null;
        }
    }
}
