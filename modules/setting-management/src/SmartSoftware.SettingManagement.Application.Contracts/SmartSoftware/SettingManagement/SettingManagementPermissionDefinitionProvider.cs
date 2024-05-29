using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Localization;
using SmartSoftware.SettingManagement.Localization;

namespace SmartSoftware.SettingManagement;

public class SettingManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var moduleGroup = context.AddGroup(SettingManagementPermissions.GroupName, L("Permission:SettingManagement"));

        var emailPermission = moduleGroup
            .AddPermission(SettingManagementPermissions.Emailing, L("Permission:Emailing"));
        emailPermission.StateCheckers.Add(new AllowChangingEmailSettingsFeatureSimpleStateChecker());

        emailPermission.AddChild(SettingManagementPermissions.EmailingTest, L("Permission:EmailingTest"));

        moduleGroup.AddPermission(SettingManagementPermissions.TimeZone, L("Permission:TimeZone"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SmartSoftwareSettingManagementResource>(name);
    }
}
