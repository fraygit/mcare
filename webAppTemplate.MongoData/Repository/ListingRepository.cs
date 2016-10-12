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
    public class ListingRepository : EntityService<Listing>, IListingRepository
    {
        public async Task<List<Listing>> GetAllCurrentActive()
        {
            var builder = Builders<Listing>.Filter;
            var filter = builder.AnyLte("DateExpiry", DateTime.UtcNow);
            var listings = await ConnectionHandler.MongoCollection.Find(filter).ToListAsync();
            return listings;
        }
    }
}
