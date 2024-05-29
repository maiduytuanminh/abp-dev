namespace SmartSoftware.MongoDB;

public interface IMongoModelSource
{
    MongoDbContextModel GetModel(SmartSoftwareMongoDbContext dbContext);
}
