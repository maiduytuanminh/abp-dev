using SmartSoftware.Localization;
using SmartSoftware.Localization.ExceptionHandling;
using SmartSoftware.Modularity;
using SmartSoftware.Docs.Localization;

namespace SmartSoftware.Docs
{
    [DependsOn(typeof(SmartSoftwareLocalizationModule))]
    public class DocsDomainSharedModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<SmartSoftwareLocalizationOptions>(options =>
            {
                options.Resources.Add<DocsResource>("en");
            });
            
            Configure<SmartSoftwareExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("SmartSoftware.Docs.Domain", typeof(DocsResource));
            });
        }
    }
}
