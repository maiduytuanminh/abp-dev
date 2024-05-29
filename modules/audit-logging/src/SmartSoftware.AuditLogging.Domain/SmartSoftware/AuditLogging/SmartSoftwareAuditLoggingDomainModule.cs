using SmartSoftware.Auditing;
using SmartSoftware.Domain;
using SmartSoftware.ExceptionHandling;
using SmartSoftware.Json;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.ObjectExtending.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.AuditLogging;

[DependsOn(typeof(SmartSoftwareAuditingModule))]
[DependsOn(typeof(SmartSoftwareDddDomainModule))]
[DependsOn(typeof(SmartSoftwareAuditLoggingDomainSharedModule))]
[DependsOn(typeof(SmartSoftwareExceptionHandlingModule))]
[DependsOn(typeof(SmartSoftwareJsonModule))]
public class SmartSoftwareAuditLoggingDomainModule : SmartSoftwareModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
            AuditLoggingModuleExtensionConsts.ModuleName,
            AuditLoggingModuleExtensionConsts.EntityNames.AuditLog,
            typeof(AuditLog)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                AuditLoggingModuleExtensionConsts.ModuleName,
                AuditLoggingModuleExtensionConsts.EntityNames.AuditLogAction,
                typeof(AuditLogAction)
            );

            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                AuditLoggingModuleExtensionConsts.ModuleName,
                AuditLoggingModuleExtensionConsts.EntityNames.EntityChange,
                typeof(EntityChange)
            );
        });
    }
}
