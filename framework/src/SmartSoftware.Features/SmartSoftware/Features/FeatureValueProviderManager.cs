using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Features;

public class FeatureValueProviderManager : IFeatureValueProviderManager, ISingletonDependency
{
    public IReadOnlyList<IFeatureValueProvider> ValueProviders => _lazyProviders.Value;
    private readonly Lazy<List<IFeatureValueProvider>> _lazyProviders;

    protected SmartSoftwareFeatureOptions Options { get; }
    protected IServiceProvider ServiceProvider { get; }

    public FeatureValueProviderManager(
        IServiceProvider serviceProvider,
        IOptions<SmartSoftwareFeatureOptions> options)
    {
        Options = options.Value;
        ServiceProvider = serviceProvider;

        _lazyProviders = new Lazy<List<IFeatureValueProvider>>(GetProviders, true);
    }
    
    protected virtual List<IFeatureValueProvider> GetProviders()
    {
        var providers = Options
            .ValueProviders
            .Select(type => (ServiceProvider.GetRequiredService(type) as IFeatureValueProvider)!)
            .ToList();
        
        var multipleProviders = providers.GroupBy(p => p.Name).FirstOrDefault(x => x.Count() > 1);
        if(multipleProviders != null)
        {
            throw new SmartSoftwareException($"Duplicate feature value provider name detected: {multipleProviders.Key}. Providers:{Environment.NewLine}{multipleProviders.Select(p => p.GetType().FullName!).JoinAsString(Environment.NewLine)}");
        }

        return providers;
    }
}