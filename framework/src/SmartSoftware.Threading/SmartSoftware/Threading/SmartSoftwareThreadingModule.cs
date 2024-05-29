using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;

namespace SmartSoftware.Threading;

public class SmartSoftwareThreadingModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<ICancellationTokenProvider>(NullCancellationTokenProvider.Instance);
        context.Services.AddSingleton(typeof(IAmbientScopeProvider<>), typeof(AmbientDataContextAmbientScopeProvider<>));
    }
}
