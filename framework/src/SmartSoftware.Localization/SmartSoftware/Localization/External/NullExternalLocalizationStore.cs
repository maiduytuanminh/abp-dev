using System;
using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Localization.External;

public class NullExternalLocalizationStore : IExternalLocalizationStore, ISingletonDependency
{
    public LocalizationResourceBase? GetResourceOrNull(string resourceName)
    {
        return null;
    }

    public Task<LocalizationResourceBase?> GetResourceOrNullAsync(string resourceName)
    {
        return Task.FromResult<LocalizationResourceBase?>(null);
    }

    public Task<string[]> GetResourceNamesAsync()
    {
        return Task.FromResult(Array.Empty<string>());
    }

    public Task<LocalizationResourceBase[]> GetResourcesAsync()
    {
        return Task.FromResult(Array.Empty<LocalizationResourceBase>());
    }
}