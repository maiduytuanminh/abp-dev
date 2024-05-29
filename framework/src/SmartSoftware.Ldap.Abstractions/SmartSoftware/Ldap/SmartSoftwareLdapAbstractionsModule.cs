using SmartSoftware.Ldap.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.Settings;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Ldap;

[DependsOn(
    typeof(SmartSoftwareVirtualFileSystemModule),
    typeof(SmartSoftwareLocalizationModule))]
public class SmartSoftwareLdapAbstractionsModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareLdapAbstractionsModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<LdapResource>("en")
                .AddVirtualJson("/SmartSoftware/Ldap/Localization");
        });
    }
}
