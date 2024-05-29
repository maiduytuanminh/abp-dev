using SmartSoftware.AuditLogging.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.Validation;
using SmartSoftware.Validation.Localization;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.AuditLogging;

[DependsOn(typeof(SmartSoftwareValidationModule))]
public class SmartSoftwareAuditLoggingDomainSharedModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareAuditLoggingDomainSharedModule>();
        });
        
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources.Add<AuditLoggingResource>("en")
                .AddBaseTypes(typeof(SmartSoftwareValidationResource))
                .AddVirtualJson("/SmartSoftware/AuditLogging/Localization");
        });
    }
}
