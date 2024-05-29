using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.CmsKit.Admin;

[DependsOn(
    typeof(CmsKitAdminApplicationContractsModule),
    typeof(CmsKitCommonHttpApiClientModule))]
public class CmsKitAdminHttpApiClientModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(
            typeof(CmsKitAdminApplicationContractsModule).Assembly,
            CmsKitAdminRemoteServiceConsts.RemoteServiceName
        );

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitAdminHttpApiClientModule>();
        });
    }
}
