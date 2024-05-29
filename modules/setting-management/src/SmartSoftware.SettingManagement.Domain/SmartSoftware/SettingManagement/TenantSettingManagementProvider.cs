using SmartSoftware.DependencyInjection;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Settings;

namespace SmartSoftware.SettingManagement;

public class TenantSettingManagementProvider : SettingManagementProvider, ITransientDependency
{
    public override string Name => TenantSettingValueProvider.ProviderName;

    protected ICurrentTenant CurrentTenant { get; }

    public TenantSettingManagementProvider(
        ISettingManagementStore settingManagementStore,
        ICurrentTenant currentTenant)
        : base(settingManagementStore)
    {
        CurrentTenant = currentTenant;
    }

    protected override string NormalizeProviderKey(string providerKey)
    {
        if (providerKey != null)
        {
            return providerKey;
        }

        return CurrentTenant.Id?.ToString();
    }
}
