using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.OpenIddict;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Account.Web;

[DependsOn(
    typeof(SmartSoftwareAccountWebModule),
    typeof(SmartSoftwareOpenIddictAspNetCoreModule)
)]
public class SmartSoftwareAccountWebOpenIddictModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareAccountWebOpenIddictModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAccountWebOpenIddictModule>();
        });
    }
}
