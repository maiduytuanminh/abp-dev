using SmartSoftware.EntityFrameworkCore.SqlServer;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.SettingManagement.EntityFrameworkCore;
using SmartSoftware.Docs.EntityFrameworkCore;

namespace SmartSoftwareDocs.EntityFrameworkCore
{
    [DependsOn(
        typeof(DocsEntityFrameworkCoreModule),
        typeof(SmartSoftwareIdentityEntityFrameworkCoreModule),
        typeof(SmartSoftwarePermissionManagementEntityFrameworkCoreModule),
        typeof(SmartSoftwareSettingManagementEntityFrameworkCoreModule),
        typeof(SmartSoftwareEntityFrameworkCoreSqlServerModule))]
    public class SmartSoftwareDocsEntityFrameworkCoreModule : SmartSoftwareModule
    {
        
    }
}
