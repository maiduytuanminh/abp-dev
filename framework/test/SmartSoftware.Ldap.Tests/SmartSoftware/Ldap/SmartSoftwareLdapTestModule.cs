using SmartSoftware.Autofac;
using SmartSoftware.Modularity;
using SmartSoftware.Settings;

namespace SmartSoftware.Ldap;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareLdapModule),
    typeof(SmartSoftwareTestBaseModule)
)]
public class SmartSoftwareLdapTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareSettingOptions>(options =>
        {
            options.ValueProviders.Add<TestLdapSettingValueProvider>();
        });
    }
}
