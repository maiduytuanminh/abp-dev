using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.Validation;
using SmartSoftware.Validation.Localization;
using SmartSoftware.Blogging.Localization;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Blogging
{
    [DependsOn(typeof(SmartSoftwareValidationModule))]
    public class BloggingDomainSharedModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BloggingDomainSharedModule>();
            });

            Configure<SmartSoftwareLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<BloggingResource>("en")
                    .AddBaseTypes(typeof(SmartSoftwareValidationResource))
                    .AddVirtualJson("SmartSoftware/Blogging/Localization/Resources");
            });
        }
    }
}
