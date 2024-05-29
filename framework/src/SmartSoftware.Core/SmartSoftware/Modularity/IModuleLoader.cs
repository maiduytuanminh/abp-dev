using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity.PlugIns;

namespace SmartSoftware.Modularity;

public interface IModuleLoader
{
    [NotNull]
    ISmartSoftwareModuleDescriptor[] LoadModules(
        [NotNull] IServiceCollection services,
        [NotNull] Type startupModuleType,
        [NotNull] PlugInSourceList plugInSources
    );
}
