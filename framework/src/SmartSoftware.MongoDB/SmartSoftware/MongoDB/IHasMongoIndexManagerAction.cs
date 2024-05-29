using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SmartSoftware.MongoDB;

public interface IHasMongoIndexManagerAction
{
    Action<IMongoIndexManager<BsonDocument>>? IndexesAction { get; set; }
}