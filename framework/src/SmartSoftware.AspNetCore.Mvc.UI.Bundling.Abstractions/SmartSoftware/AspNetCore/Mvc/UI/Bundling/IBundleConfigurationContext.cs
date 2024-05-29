using System.Collections.Generic;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling;

public interface IBundleConfigurationContext : IServiceProviderAccessor
{
    List<BundleFile> Files { get; }
}
