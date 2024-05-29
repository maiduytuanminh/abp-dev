using System;
using SmartSoftware.ObjectExtending.Modularity;

namespace SmartSoftware.ObjectExtending;

public static class IdentityModuleExtensionConfigurationDictionaryExtensions
{
    public static ModuleExtensionConfigurationDictionary ConfigureIdentity(
        this ModuleExtensionConfigurationDictionary modules,
        Action<IdentityModuleExtensionConfiguration> configureAction)
    {
        return modules.ConfigureModule(
            IdentityModuleExtensionConsts.ModuleName,
            configureAction
        );
    }
}
