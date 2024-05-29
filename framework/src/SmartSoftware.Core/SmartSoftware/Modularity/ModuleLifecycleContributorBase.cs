using System.Threading.Tasks;

namespace SmartSoftware.Modularity;

public abstract class ModuleLifecycleContributorBase : IModuleLifecycleContributor
{
    public virtual Task InitializeAsync(ApplicationInitializationContext context, ISmartSoftwareModule module)
    {
        return Task.CompletedTask;
    }

    public virtual void Initialize(ApplicationInitializationContext context, ISmartSoftwareModule module)
    {
    }

    public virtual Task ShutdownAsync(ApplicationShutdownContext context, ISmartSoftwareModule module)
    {
        return Task.CompletedTask;
    }

    public virtual void Shutdown(ApplicationShutdownContext context, ISmartSoftwareModule module)
    {
    }
}
