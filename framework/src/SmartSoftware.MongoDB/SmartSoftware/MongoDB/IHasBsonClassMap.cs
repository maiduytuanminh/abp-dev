using MongoDB.Bson.Serialization;

namespace SmartSoftware.MongoDB;

public interface IHasBsonClassMap
{
    BsonClassMap GetMap();
}
