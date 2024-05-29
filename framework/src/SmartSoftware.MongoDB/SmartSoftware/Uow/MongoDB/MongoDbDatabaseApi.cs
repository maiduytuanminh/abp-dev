using SmartSoftware.MongoDB;

namespace SmartSoftware.Uow.MongoDB;

public class MongoDbDatabaseApi : IDatabaseApi
{
    public ISmartSoftwareMongoDbContext DbContext { get; }

    public MongoDbDatabaseApi(ISmartSoftwareMongoDbContext dbContext)
    {
        DbContext = dbContext;
    }
}
