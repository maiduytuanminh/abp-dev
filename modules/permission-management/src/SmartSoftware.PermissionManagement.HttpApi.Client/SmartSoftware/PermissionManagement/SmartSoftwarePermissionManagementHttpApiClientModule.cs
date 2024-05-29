using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.PermissionManagement;

[DependsOn(
    typeof(SmartSoftwarePermissionManagementApplicationContractsModule),
    typeof(SmartSoftwareHttpClientModule))]
public class SmartSoftwarePermissionManagementHttpApiClientModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(
            typeof(SmartSoftwarePermissionManagementApplicationContractsModule).Assembly,
            PermissionManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwarePermissionManagementHttpApiClientModule>();
        });
    }
}
