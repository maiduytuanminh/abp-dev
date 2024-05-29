using MongoDB.Driver;
using SmartSoftware.EntityFrameworkCore.TestApp.FourthContext;

namespace SmartSoftware.MongoDB.TestApp.FourthContext;

/* This dbcontext is just for testing to replace dbcontext from the application using ReplaceDbContextAttribute
 */
public class FourthDbContext : SmartSoftwareMongoDbContext, IFourthDbContext
{
    public IMongoCollection<FourthDbContextDummyEntity> FourthDummyEntities => Collection<FourthDbContextDummyEntity>();

}
