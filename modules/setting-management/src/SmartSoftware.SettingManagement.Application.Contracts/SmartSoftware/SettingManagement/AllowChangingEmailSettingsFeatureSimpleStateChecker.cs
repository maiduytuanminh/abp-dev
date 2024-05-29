using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Features;
using SmartSoftware.MultiTenancy;
using SmartSoftware.SimpleStateChecking;

namespace SmartSoftware.SettingManagement;

public class AllowChangingEmailSettingsFeatureSimpleStateChecker : ISimpleStateChecker<PermissionDefinition>
{
    public async Task<bool> IsEnabledAsync(SimpleStateCheckerContext<PermissionDefinition> context)
    {
        var currentTenant = context.ServiceProvider.GetRequiredService<ICurrentTenant>();

        if (!currentTenant.IsAvailable)
        {
            return true;
        }

        var featureChecker = context.ServiceProvider.GetRequiredService<IFeatureChecker>();
        return await featureChecker.IsEnabledAsync(SettingManagementFeatures.AllowChangingEmailSettings);
    }
}
