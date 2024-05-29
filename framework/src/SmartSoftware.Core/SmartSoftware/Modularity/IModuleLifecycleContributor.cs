using System.Threading.Tasks;
using JetBrains.Annotations;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Modularity;

public interface IModuleLifecycleContributor : ITransientDependency
{
    Task InitializeAsync([NotNull] ApplicationInitializationContext context, [NotNull] ISmartSoftwareModule module);

    void Initialize([NotNull] ApplicationInitializationContext context, [NotNull] ISmartSoftwareModule module);

    Task ShutdownAsync([NotNull] ApplicationShutdownContext context, [NotNull] ISmartSoftwareModule module);

    void Shutdown([NotNull] ApplicationShutdownContext context, [NotNull] ISmartSoftwareModule module);
}
