using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mcare.MongoData.Entities.Base
{
    public class MongoEntity : IMongoEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
