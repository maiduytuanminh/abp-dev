using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Account;

[DependsOn(
    typeof(SmartSoftwareAccountApplicationContractsModule),
    typeof(SmartSoftwareHttpClientModule))]
public class SmartSoftwareAccountHttpApiClientModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(typeof(SmartSoftwareAccountApplicationContractsModule).Assembly,
            AccountRemoteServiceConsts.RemoteServiceName);

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAccountHttpApiClientModule>();
        });
    }
}
