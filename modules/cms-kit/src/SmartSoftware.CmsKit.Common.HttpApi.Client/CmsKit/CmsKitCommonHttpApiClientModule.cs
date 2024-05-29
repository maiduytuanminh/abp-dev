using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(SmartSoftwareHttpClientModule),
    typeof(CmsKitCommonApplicationContractsModule)
    )]
public class CmsKitCommonHttpApiClientModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(
            typeof(CmsKitCommonApplicationContractsModule).Assembly,
            CmsKitCommonRemoteServiceConsts.RemoteServiceName
        );

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitCommonHttpApiClientModule>();
        });
    }
}
