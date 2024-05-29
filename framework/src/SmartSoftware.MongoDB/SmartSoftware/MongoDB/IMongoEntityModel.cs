using System;

namespace SmartSoftware.MongoDB;

public interface IMongoEntityModel
{
    Type EntityType { get; }

    string CollectionName { get; }
}
