using SmartSoftware.Modularity;
using SmartSoftware.Settings;

namespace SmartSoftware.Ldap;

[DependsOn(
    typeof(SmartSoftwareLdapAbstractionsModule),
    typeof(SmartSoftwareSettingsModule))]
public class SmartSoftwareLdapModule : SmartSoftwareModule
{
   
}
