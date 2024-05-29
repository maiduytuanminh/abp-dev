using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.FeatureManagement;

public class NextTenantFeatureManagementProvider : FeatureManagementProvider, ITransientDependency
{
    public static string ProviderName => "TENANT";

    public override string Name => ProviderName;

    protected ICurrentTenant CurrentTenant { get; }

    public NextTenantFeatureManagementProvider(
        IFeatureManagementStore store,
        ICurrentTenant currentTenant)
        : base(store)
    {
        CurrentTenant = currentTenant;
    }

    protected override Task<string> NormalizeProviderKeyAsync(string providerKey)
    {
        if (providerKey != null)
        {
            return Task.FromResult(providerKey);
        }

        return Task.FromResult(CurrentTenant.Id?.ToString());
    }
}
