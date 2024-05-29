using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.Settings;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareSettingsModule),
    typeof(SmartSoftwareTestBaseModule)
    )]
public class SmartSoftwareSettingsTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareSettingOptions>(options =>
        {
            options.ValueProviders.Add<TestSettingValueProvider>();
        });
    }
}
