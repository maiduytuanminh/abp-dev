using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.BlobStoring.Database.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.SqlServer;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.SettingManagement.EntityFrameworkCore;
using SmartSoftware.Blogging.EntityFrameworkCore;

namespace SmartSoftware.BloggingTestApp.EntityFrameworkCore
{
    [DependsOn(
        typeof(BloggingEntityFrameworkCoreModule),
        typeof(SmartSoftwareIdentityEntityFrameworkCoreModule),
        typeof(SmartSoftwarePermissionManagementEntityFrameworkCoreModule),
        typeof(SmartSoftwareSettingManagementEntityFrameworkCoreModule),
        typeof(SmartSoftwareEntityFrameworkCoreSqlServerModule),
        typeof(BlobStoringDatabaseEntityFrameworkCoreModule))]
    public class BloggingTestAppEntityFrameworkCoreModule : SmartSoftwareModule
    {
    }
}
