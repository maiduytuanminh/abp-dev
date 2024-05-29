using SmartSoftware.Data;
using SmartSoftware.MongoDB;

namespace MyCompanyName.MyProjectName.MongoDB;

[ConnectionStringName("Default")]
public class MyProjectNameMongoDbContext : SmartSoftwareMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        //modelBuilder.Entity<YourEntity>(b =>
        //{
        //    //...
        //});
    }
}
