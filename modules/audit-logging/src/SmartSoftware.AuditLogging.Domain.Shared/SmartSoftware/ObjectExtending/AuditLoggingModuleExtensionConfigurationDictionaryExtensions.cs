using System;
using SmartSoftware.ObjectExtending.Modularity;

namespace SmartSoftware.ObjectExtending;

public static class AuditLoggingModuleExtensionConfigurationDictionaryExtensions
{
    public static ModuleExtensionConfigurationDictionary ConfigureAuditLogging(
        this ModuleExtensionConfigurationDictionary modules,
        Action<AuditLoggingModuleExtensionConfiguration> configureAction)
    {
        return modules.ConfigureModule(
            AuditLoggingModuleExtensionConsts.ModuleName,
            configureAction
        );
    }
}
