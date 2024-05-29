using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Authorization;
using SmartSoftware.GlobalFeatures.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Localization.ExceptionHandling;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.GlobalFeatures;

[DependsOn(
    typeof(SmartSoftwareLocalizationModule),
    typeof(SmartSoftwareVirtualFileSystemModule),
    typeof(SmartSoftwareAuthorizationAbstractionsModule)
)]
public class SmartSoftwareGlobalFeaturesModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(GlobalFeatureInterceptorRegistrar.RegisterIfNeeded);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareGlobalFeatureResource>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<SmartSoftwareGlobalFeatureResource>("en")
                .AddVirtualJson("/SmartSoftware/GlobalFeatures/Localization");
        });

        Configure<SmartSoftwareExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("SmartSoftware.GlobalFeature", typeof(SmartSoftwareGlobalFeatureResource));
        });
    }
}
