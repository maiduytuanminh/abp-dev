using MongoDB.Bson;
using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.DependencyInjection;
using SmartSoftware.EntityFrameworkCore.TestApp.FourthContext;
using SmartSoftware.MongoDB;
using SmartSoftware.MongoDB.TestApp.FourthContext;
using SmartSoftware.MongoDB.TestApp.ThirdDbContext;
using SmartSoftware.TestApp.Domain;
using SmartSoftware.TestApp.Testing;

namespace SmartSoftware.TestApp.MongoDB;

[ConnectionStringName("TestApp")]
[ReplaceDbContext(typeof(IFourthDbContext))]
public class TestAppMongoDbContext : SmartSoftwareMongoDbContext, ITestAppMongoDbContext, IThirdDbContext, IFourthDbContext
{
    [MongoCollection("Persons")] //Intentionally changed the collection name to test it
    public IMongoCollection<Person> People => Collection<Person>();

    public IMongoCollection<EntityWithIntPk> EntityWithIntPks => Collection<EntityWithIntPk>();

    public IMongoCollection<City> Cities => Collection<City>();

    public IMongoCollection<ThirdDbContextDummyEntity> DummyEntities => Collection<ThirdDbContextDummyEntity>();

    public IMongoCollection<FourthDbContextDummyEntity> FourthDummyEntities => Collection<FourthDbContextDummyEntity>();

    public IMongoCollection<Product> Products => Collection<Product>();

    public IMongoCollection<AppEntityWithNavigations> AppEntityWithNavigations => Collection<AppEntityWithNavigations>();

    protected internal override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.Entity<City>(b =>
        {
            b.CollectionName = "MyCities";
        });
        
        modelBuilder.Entity<Person>(b =>
        {
            b.CreateCollectionOptions.Collation = new Collation(locale:"en_US", strength: CollationStrength.Secondary);
        });
    }
}
