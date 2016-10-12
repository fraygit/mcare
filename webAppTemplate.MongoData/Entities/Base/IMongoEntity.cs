using MongoDB.Bson;

namespace mcare.MongoData.Entities.Base
{
    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
}
