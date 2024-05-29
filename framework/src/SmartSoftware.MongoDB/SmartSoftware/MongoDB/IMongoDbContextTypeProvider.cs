using System;

namespace SmartSoftware.MongoDB;

public interface IMongoDbContextTypeProvider
{
    Type GetDbContextType(Type dbContextType);
}
