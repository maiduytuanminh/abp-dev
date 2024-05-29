using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.BootstrapDatepicker;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.JQueryValidation;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Moment;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Timeago;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages;

[DependsOn(
    typeof(SmartSoftwareAspNetCoreMvcUiBundlingAbstractionsModule),
    typeof(SmartSoftwareLocalizationModule)
    )]
public class SmartSoftwareAspNetCoreMvcUiPackagesModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            //BootstrapDatepicker
            options.AddLanguagesMapOrUpdate(BootstrapDatepickerScriptContributor.PackageName,
                new NameValue("zh-Hans", "zh-CN"),
                new NameValue("zh-Hant", "zh-TW"));

            options.AddLanguageFilesMapOrUpdate(BootstrapDatepickerScriptContributor.PackageName,
                new NameValue("zh-Hans", "zh-CN"),
                new NameValue("zh-Hant", "zh-TW"));
            
            //moment
            options.AddLanguagesMapOrUpdate(MomentScriptContributor.PackageName,
                new NameValue("zh-Hans", "zh-cn"),
                new NameValue("zh-Hant", "zh-tw"),
                new NameValue("de-DE", "de"));

            options.AddLanguageFilesMapOrUpdate(MomentScriptContributor.PackageName,
                new NameValue("zh-Hans", "zh-cn"),
                new NameValue("zh-Hant", "zh-tw"),
                new NameValue("de-DE", "de"));

            //Timeago
            options.AddLanguageFilesMapOrUpdate(TimeagoScriptContributor.PackageName,
                new NameValue("zh-Hans", "zh-CN"),
                new NameValue("zh-Hant", "zh-TW"));

            //JQueryValidation
            options.AddLanguageFilesMapOrUpdate(JQueryValidationScriptContributor.PackageName,
                new NameValue("zh-Hans", "zh"),
                new NameValue("zh-Hant", "zh_TW"));
        });
    }
}
