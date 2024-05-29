using MongoDB.Driver;

namespace SmartSoftware.MongoDB.TestApp.ThirdDbContext;

/* This dbcontext is just for testing to replace dbcontext from the application using SmartSoftwareDbContextRegistrationOptions.ReplaceDbContext
 */
public class ThirdDbContext : SmartSoftwareMongoDbContext, IThirdDbContext
{
    public IMongoCollection<ThirdDbContextDummyEntity> DummyEntities => Collection<ThirdDbContextDummyEntity>();
}
