using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using SmartSoftware.Features;
using SmartSoftware.SettingManagement.Web.Pages.SettingManagement;
using SmartSoftware.UI.Navigation;
using SmartSoftware.SettingManagement.Localization;

namespace SmartSoftware.SettingManagement.Web.Navigation;

public class SettingManagementMainMenuContributor : IMenuContributor
{
    public virtual async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return;
        }

        if (context.Menu.FindMenuItem(SettingManagementMenuNames.GroupName) != null)
        {
            /* This may happen if blazor server UI is being used in the same application.
             * In this case, we don't add the MVC setting management UI. */
            return;
        }

        var settingPageContributorManager = context.ServiceProvider.GetRequiredService<SettingPageContributorManager>();
        if (!(await settingPageContributorManager.GetAvailableContributors()).Any())
        {
            return;
        }

        var l = context.GetLocalizer<SmartSoftwareSettingManagementResource>();

        context.Menu
            .GetAdministration()
            .AddItem(
                new ApplicationMenuItem(
                    SettingManagementMenuNames.GroupName,
                    l["Settings"],
                    "~/SettingManagement",
                    icon: "fa fa-cog"
                ).RequireFeatures(SettingManagementFeatures.Enable)
            );
    }
}
