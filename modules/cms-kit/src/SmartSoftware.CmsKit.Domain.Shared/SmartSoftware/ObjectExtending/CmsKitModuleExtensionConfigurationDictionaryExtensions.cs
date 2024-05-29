using System;
using SmartSoftware.ObjectExtending.Modularity;

namespace SmartSoftware.ObjectExtending;

public static class CmsKitModuleExtensionConfigurationDictionaryExtensions
{
    public static ModuleExtensionConfigurationDictionary ConfigureCmsKit(
        this ModuleExtensionConfigurationDictionary modules,
        Action<CmsKitModuleExtensionConfiguration> configureAction)
    {
        return modules.ConfigureModule(
            CmsKitModuleExtensionConsts.ModuleName,
            configureAction
        );
    }
}