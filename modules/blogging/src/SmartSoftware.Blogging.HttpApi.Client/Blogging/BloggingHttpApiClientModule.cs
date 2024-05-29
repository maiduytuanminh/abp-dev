using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Blogging
{
    [DependsOn(
        typeof(BloggingApplicationContractsModule),
        typeof(SmartSoftwareHttpClientModule))]
    public class BloggingHttpApiClientModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStaticHttpClientProxies(typeof(BloggingApplicationContractsModule).Assembly,
                BloggingRemoteServiceConsts.RemoteServiceName);

            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BloggingHttpApiClientModule>();
            });
        }

    }
}
