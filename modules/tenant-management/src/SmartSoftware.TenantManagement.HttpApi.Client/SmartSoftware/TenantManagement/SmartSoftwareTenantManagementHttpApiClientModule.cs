using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.TenantManagement;

[DependsOn(
    typeof(SmartSoftwareTenantManagementApplicationContractsModule),
    typeof(SmartSoftwareHttpClientModule))]
public class SmartSoftwareTenantManagementHttpApiClientModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(
            typeof(SmartSoftwareTenantManagementApplicationContractsModule).Assembly,
            TenantManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareTenantManagementHttpApiClientModule>();
        });
    }
}
