using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Dapr;
using SmartSoftware.Modularity;

namespace SmartSoftware.Http.Client.Dapr;

[DependsOn(
    typeof(SmartSoftwareHttpClientModule),
    typeof(SmartSoftwareDaprModule)
)]
public class SmartSoftwareHttpClientDaprModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<SmartSoftwareHttpClientBuilderOptions>(options =>
        {
            options.ProxyClientBuildActions.Add((_, clientBuilder) =>
            {
                clientBuilder.AddHttpMessageHandler<SmartSoftwareInvocationHandler>();
            });
        });
    }
}