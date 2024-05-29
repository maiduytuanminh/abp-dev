using SmartSoftware.Data;
using SmartSoftware.MongoDB;

namespace MyCompanyName.MyProjectName.Data;

[ConnectionStringName("Default")]
public class MyProjectNameDbContext : SmartSoftwareMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        //builder.Entity<YourEntity>(b =>
        //{
        //    //...
        //});
    }
}
