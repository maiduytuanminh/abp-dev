using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.WebAssembly;

public class ApplicationConfigurationCache : ISingletonDependency
{
    protected ApplicationConfigurationDto? Configuration { get; set; }

    public virtual ApplicationConfigurationDto? Get()
    {
        return Configuration;
    }

    public void Set(ApplicationConfigurationDto configuration)
    {
        Configuration = configuration;
    }
}
