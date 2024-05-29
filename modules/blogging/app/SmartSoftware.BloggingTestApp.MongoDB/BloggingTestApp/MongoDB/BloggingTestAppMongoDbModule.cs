using SmartSoftware.BlobStoring.Database.MongoDB;
using SmartSoftware.Identity.MongoDB;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.MongoDB;
using SmartSoftware.SettingManagement.MongoDB;
using SmartSoftware.Blogging.MongoDB;

namespace SmartSoftware.BloggingTestApp.MongoDB
{
    [DependsOn(
        typeof(SmartSoftwareIdentityMongoDbModule),
        typeof(BloggingMongoDbModule),
        typeof(SmartSoftwareSettingManagementMongoDbModule),
        typeof(SmartSoftwarePermissionManagementMongoDbModule),
        typeof(BlobStoringDatabaseMongoDbModule)
    )]
    public class BloggingTestAppMongoDbModule : SmartSoftwareModule
    {
    }
}
