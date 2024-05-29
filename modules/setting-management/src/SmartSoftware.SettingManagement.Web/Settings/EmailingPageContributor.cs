using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using SmartSoftware.SettingManagement.Localization;
using SmartSoftware.SettingManagement.Web.Pages.SettingManagement;
using SmartSoftware.SettingManagement.Web.Pages.SettingManagement.Components.EmailSettingGroup;

namespace SmartSoftware.SettingManagement.Web.Settings;

public class EmailingPageContributor : SettingPageContributorBase
{
    public EmailingPageContributor()
    {
        RequiredTenantSideFeatures(SettingManagementFeatures.Enable);
        RequiredTenantSideFeatures(SettingManagementFeatures.AllowChangingEmailSettings);
        RequiredPermissions(SettingManagementPermissions.Emailing);
    }
    public override Task ConfigureAsync(SettingPageCreationContext context)
    {
        var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<SmartSoftwareSettingManagementResource>>();
        context.Groups.Add(
            new SettingPageGroup(
                "SmartSoftware.EmailSetting",
                l["Menu:Emailing"],
                typeof(EmailSettingGroupViewComponent)
            )
        );
        return Task.CompletedTask;
    }
}
