using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Autofac;
using SmartSoftware.AutoMapper;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.ObjectMapping;
using SmartSoftware.Settings;

namespace SmartSoftware.MultiLingualObjects;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareLocalizationModule),
    typeof(SmartSoftwareSettingsModule),
    typeof(SmartSoftwareObjectMappingModule),
    typeof(SmartSoftwareMultiLingualObjectsModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareAutoMapperModule)
)]
public class SmartSoftwareMultiLingualObjectsTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareSettingOptions>(options =>
        {   
            options.DefinitionProviders.Add<LocalizationSettingProvider>();
        });
        context.Services.AddAutoMapperObjectMapper<SmartSoftwareMultiLingualObjectsTestModule>();
        Configure<SmartSoftwareAutoMapperOptions>(options =>
        {
            options.AddMaps<SmartSoftwareMultiLingualObjectsTestModule>(validate: true);
        });
    }
}
