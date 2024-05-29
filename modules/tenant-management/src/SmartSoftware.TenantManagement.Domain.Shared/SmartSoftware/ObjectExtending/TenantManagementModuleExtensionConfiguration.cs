using System;
using SmartSoftware.ObjectExtending.Modularity;

namespace SmartSoftware.ObjectExtending;

public class TenantManagementModuleExtensionConfiguration : ModuleExtensionConfiguration
{
    public TenantManagementModuleExtensionConfiguration ConfigureTenant(
        Action<EntityExtensionConfiguration> configureAction)
    {
        return this.ConfigureEntity(
            TenantManagementModuleExtensionConsts.EntityNames.Tenant,
            configureAction
        );
    }
}
