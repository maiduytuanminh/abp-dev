using System;
using System.Collections.Generic;

namespace SmartSoftware.MongoDB;

public class MongoDbContextModel
{
    public IReadOnlyDictionary<Type, IMongoEntityModel> Entities { get; }

    public MongoDbContextModel(IReadOnlyDictionary<Type, IMongoEntityModel> entities)
    {
        Entities = entities;
    }
}
