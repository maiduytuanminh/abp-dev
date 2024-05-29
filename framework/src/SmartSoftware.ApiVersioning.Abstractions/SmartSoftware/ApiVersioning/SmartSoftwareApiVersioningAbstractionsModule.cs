using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;

namespace SmartSoftware.ApiVersioning;

public class SmartSoftwareApiVersioningAbstractionsModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IRequestedApiVersion>(NullRequestedApiVersion.Instance);
    }
}
