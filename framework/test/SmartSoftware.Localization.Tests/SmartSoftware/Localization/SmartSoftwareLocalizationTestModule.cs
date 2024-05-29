using SmartSoftware.Localization.TestResources.Base.Validation;
using SmartSoftware.Localization.TestResources.External;
using SmartSoftware.Localization.TestResources.Source;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Localization;

[DependsOn(typeof(SmartSoftwareTestBaseModule))]
[DependsOn(typeof(SmartSoftwareLocalizationModule))]
public class SmartSoftwareLocalizationTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareLocalizationTestModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.DefaultResourceType = typeof(LocalizationTestResource);

            options.Resources
                .Add<LocalizationTestValidationResource>("en")
                .AddVirtualJson("/SmartSoftware/Localization/TestResources/Base/Validation");

            options.Resources
                .Add("LocalizationTestCountryNames")
                .AddVirtualJson("/SmartSoftware/Localization/TestResources/Base/CountryNames");

            options.Resources
                .Add<LocalizationTestResource>("en")
                .AddVirtualJson("/SmartSoftware/Localization/TestResources/Source")
                .AddBaseResources("LocalizationTestCountryNames");

            options.Resources
                .Get<LocalizationTestResource>()
                .AddVirtualJson("/SmartSoftware/Localization/TestResources/SourceExt");

            options.GlobalContributors.Add<TestExternalLocalizationContributor>();
        });
    }
}
