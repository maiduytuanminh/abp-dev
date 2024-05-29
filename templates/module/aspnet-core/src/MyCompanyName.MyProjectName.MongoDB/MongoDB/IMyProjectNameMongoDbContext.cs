using SmartSoftware.Data;
using SmartSoftware.MongoDB;

namespace MyCompanyName.MyProjectName.MongoDB;

[ConnectionStringName(MyProjectNameDbProperties.ConnectionStringName)]
public interface IMyProjectNameMongoDbContext : ISmartSoftwareMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
