using System;
using SmartSoftware.ObjectExtending.Modularity;

namespace SmartSoftware.ObjectExtending;

public static class OpenIddictModuleExtensionConfigurationDictionaryExtensions
{
    public static ModuleExtensionConfigurationDictionary ConfigureOpenIddict(
        this ModuleExtensionConfigurationDictionary modules,
        Action<OpenIddictModuleExtensionConfiguration> configureAction)
    {
        return modules.ConfigureModule(
            OpenIddictModuleExtensionConsts.ModuleName,
            configureAction
        );
    }
}
