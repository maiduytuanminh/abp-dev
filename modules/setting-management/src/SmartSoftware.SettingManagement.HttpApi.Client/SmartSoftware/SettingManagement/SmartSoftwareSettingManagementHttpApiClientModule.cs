using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.SettingManagement;

[DependsOn(
    typeof(SmartSoftwareSettingManagementApplicationContractsModule),
    typeof(SmartSoftwareHttpClientModule))]
public class SmartSoftwareSettingManagementHttpApiClientModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(
            typeof(SmartSoftwareSettingManagementApplicationContractsModule).Assembly,
            SettingManagementRemoteServiceConsts.RemoteServiceName
        );

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareSettingManagementHttpApiClientModule>();
        });
    }
}
