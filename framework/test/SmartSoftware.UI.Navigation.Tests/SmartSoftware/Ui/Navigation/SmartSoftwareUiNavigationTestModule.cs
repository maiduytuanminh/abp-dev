using SmartSoftware.Authorization;
using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.UI.Navigation;

[DependsOn(typeof(SmartSoftwareUiNavigationModule))]
[DependsOn(typeof(SmartSoftwareAuthorizationModule))]
[DependsOn(typeof(SmartSoftwareAutofacModule))]
public class SmartSoftwareUiNavigationTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new MenuManager_Tests.TestMenuContributor1());
            options.MenuContributors.Add(new MenuManager_Tests.TestMenuContributor2());
            options.MenuContributors.Add(new MenuManager_Tests.TestMenuContributor3());
            options.MenuContributors.Add(new MenuManager_Tests.TestMenuContributor4());

            options.MainMenuNames.Add(MenuManager_Tests.TestMenuContributor3.MenuName);
        });
    }
}
