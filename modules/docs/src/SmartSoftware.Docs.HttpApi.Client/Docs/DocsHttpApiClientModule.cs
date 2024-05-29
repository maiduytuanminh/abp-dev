using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Docs
{
    [DependsOn(
        typeof(DocsApplicationContractsModule),
        typeof(SmartSoftwareHttpClientModule)
    )]
    public class DocsHttpApiClientModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStaticHttpClientProxies(typeof(DocsApplicationContractsModule).Assembly, DocsRemoteServiceConsts.RemoteServiceName);

            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DocsHttpApiClientModule>();
            });
        }
    }
}
