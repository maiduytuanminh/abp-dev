using MongoDB.Bson;
using MongoDB.Driver;

namespace SmartSoftware.MongoDB;

public interface IHasCreateCollectionOptions
{
    CreateCollectionOptions<BsonDocument> CreateCollectionOptions { get; }
}