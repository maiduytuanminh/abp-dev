using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.Http.Client.IdentityModel.Web;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.ClientSimulation;

[DependsOn(
    typeof(ClientSimulationModule),
    typeof(SmartSoftwareHttpClientIdentityModelWebModule),
    typeof(SmartSoftwareAspNetCoreMvcUiThemeSharedModule)
    )]
public class ClientSimulationWebModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ClientSimulationWebModule>("SmartSoftware.ClientSimulation");
        });
    }
}
