using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using SmartSoftware.SettingManagement.Localization;
using SmartSoftware.SettingManagement.Web.Pages.SettingManagement;
using SmartSoftware.SettingManagement.Web.Pages.SettingManagement.Components.TimeZoneSettingGroup;
using SmartSoftware.Timing;

namespace SmartSoftware.SettingManagement.Web.Settings;

public class TimeZonePageContributor : SettingPageContributorBase
{
    public TimeZonePageContributor()
    {
        RequiredPermissions(SettingManagementPermissions.TimeZone);
    }
    
    public override Task ConfigureAsync(SettingPageCreationContext context)
    {
        var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<SmartSoftwareSettingManagementResource>>();

        if (context.ServiceProvider.GetRequiredService<IClock>().SupportsMultipleTimezone)
        {
            context.Groups.Add(
                new SettingPageGroup(
                    "SmartSoftware.TimeZone",
                    l["Menu:TimeZone"],
                    typeof(TimeZoneSettingGroupViewComponent)
                )
            );
        }

        return Task.CompletedTask;
    }
}
