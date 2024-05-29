using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Account;
using SmartSoftware.FeatureManagement;
using SmartSoftware.Identity;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement;
using SmartSoftware.TenantManagement;
using SmartSoftware.SettingManagement;
using SmartSoftware.VirtualFileSystem;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    typeof(MyProjectNameApplicationContractsModule),
    typeof(SmartSoftwareAccountHttpApiClientModule),
    typeof(SmartSoftwareIdentityHttpApiClientModule),
    typeof(SmartSoftwarePermissionManagementHttpApiClientModule),
    typeof(SmartSoftwareTenantManagementHttpApiClientModule),
    typeof(SmartSoftwareFeatureManagementHttpApiClientModule),
    typeof(SmartSoftwareSettingManagementHttpApiClientModule)
)]
public class MyProjectNameHttpApiClientModule : SmartSoftwareModule
{
    public const string RemoteServiceName = "Default";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(MyProjectNameApplicationContractsModule).Assembly,
            RemoteServiceName
        );

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MyProjectNameHttpApiClientModule>();
        });
    }
}
