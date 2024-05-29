using SmartSoftware.BlobStoring.Database.EntityFrameworkCore;
using SmartSoftware.Modularity;

namespace SmartSoftware.BlobStoring.Database;

[DependsOn(
    typeof(BlobStoringDatabaseEntityFrameworkCoreTestModule)
    )]
public class BlobStoringDatabaseDomainTestModule : SmartSoftwareModule
{

}
