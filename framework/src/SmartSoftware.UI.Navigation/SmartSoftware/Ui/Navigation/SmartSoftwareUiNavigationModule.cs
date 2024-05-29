using SmartSoftware.Authorization;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.UI.Navigation.Localization.Resource;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.UI.Navigation;

[DependsOn(typeof(SmartSoftwareUiModule), typeof(SmartSoftwareAuthorizationModule), typeof(SmartSoftwareMultiTenancyModule))]
public class SmartSoftwareUiNavigationModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareUiNavigationModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<SmartSoftwareUiNavigationResource>("en")
                .AddVirtualJson("/SmartSoftware/Ui/Navigation/Localization/Resource");
        });

        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new DefaultMenuContributor());
        });
    }
}
