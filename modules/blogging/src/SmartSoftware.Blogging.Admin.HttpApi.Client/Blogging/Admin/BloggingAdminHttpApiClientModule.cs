using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Blogging.Admin
{
    [DependsOn(
        typeof(BloggingAdminApplicationContractsModule),
        typeof(SmartSoftwareHttpClientModule))]
    public class BloggingAdminHttpApiClientModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddStaticHttpClientProxies(typeof(BloggingAdminApplicationContractsModule).Assembly,
                BloggingAdminRemoteServiceConsts.RemoteServiceName);

            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BloggingAdminHttpApiClientModule>();
            });
        }

    }
}
