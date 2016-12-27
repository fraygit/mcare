using MongoDB.Bson;
using System;

namespace mcare.MongoData.Entities.Base
{
    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
        DateTime DateCreated { get; set; }
    }
}
