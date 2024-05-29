using System;
using SmartSoftware.ObjectExtending.Modularity;

namespace SmartSoftware.ObjectExtending;

public static class IdentityServerModuleExtensionConfigurationDictionaryExtensions
{
    public static ModuleExtensionConfigurationDictionary ConfigureIdentityServer(
        this ModuleExtensionConfigurationDictionary modules,
        Action<IdentityServerModuleExtensionConfiguration> configureAction)
    {
        return modules.ConfigureModule(
            IdentityServerModuleExtensionConsts.ModuleName,
            configureAction
        );
    }
}
