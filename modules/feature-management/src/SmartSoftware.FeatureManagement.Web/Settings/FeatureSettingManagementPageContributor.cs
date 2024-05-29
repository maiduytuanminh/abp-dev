using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using SmartSoftware.FeatureManagement.Localization;
using SmartSoftware.FeatureManagement.Pages.FeatureManagement.Components.FeatureSettingGroup;
using SmartSoftware.SettingManagement.Web.Pages.SettingManagement;

namespace SmartSoftware.FeatureManagement.Settings;

public class FeatureSettingManagementPageContributor : SettingPageContributorBase
{
    public FeatureSettingManagementPageContributor()
    {
        RequiredPermissions(FeatureManagementPermissions.ManageHostFeatures);
    }

    public override Task ConfigureAsync(SettingPageCreationContext context)
    {
        var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<SmartSoftwareFeatureManagementResource>>();
        context.Groups.Add(
            new SettingPageGroup(
                "SmartSoftware.FeatureManagement",
                l["Menu:FeatureManagement"],
                typeof(FeatureSettingGroupViewComponent)
            )
        );
        return Task.CompletedTask;
    }
}