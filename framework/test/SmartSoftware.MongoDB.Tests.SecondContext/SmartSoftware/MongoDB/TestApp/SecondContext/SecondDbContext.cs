using MongoDB.Driver;

namespace SmartSoftware.MongoDB.TestApp.SecondContext;

public class SecondDbContext : SmartSoftwareMongoDbContext
{
    public IMongoCollection<BookInSecondDbContext> Books => Collection<BookInSecondDbContext>();

    public IMongoCollection<PhoneInSecondDbContext> Phones => Collection<PhoneInSecondDbContext>();
}
