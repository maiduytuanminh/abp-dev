using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Docs.Admin
{
    [DependsOn(
        typeof(DocsAdminApplicationContractsModule),
        typeof(SmartSoftwareHttpClientModule))]
    public class DocsAdminHttpApiClientModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStaticHttpClientProxies(typeof(DocsAdminApplicationContractsModule).Assembly, DocsAdminRemoteServiceConsts.RemoteServiceName);

            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DocsAdminHttpApiClientModule>();
            });
        }
    }
}
