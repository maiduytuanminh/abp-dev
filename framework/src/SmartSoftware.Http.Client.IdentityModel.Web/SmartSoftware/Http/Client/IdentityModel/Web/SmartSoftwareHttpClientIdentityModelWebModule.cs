using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;

namespace SmartSoftware.Http.Client.IdentityModel.Web;

[DependsOn(
    typeof(SmartSoftwareHttpClientIdentityModelModule)
    )]
public class SmartSoftwareHttpClientIdentityModelWebModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpContextAccessor();
    }
}
