using System;
using SmartSoftware.ObjectExtending.Modularity;

namespace SmartSoftware.ObjectExtending;

public static class TenantManagementModuleExtensionConfigurationDictionaryExtensions
{
    public static ModuleExtensionConfigurationDictionary ConfigureTenantManagement(
        this ModuleExtensionConfigurationDictionary modules,
        Action<TenantManagementModuleExtensionConfiguration> configureAction)
    {
        return modules.ConfigureModule(
            TenantManagementModuleExtensionConsts.ModuleName,
            configureAction
        );
    }
}
