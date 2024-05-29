using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Localization;
using SmartSoftware.CmsKit.Localization;

namespace SmartSoftware.CmsKit.Permissions;

public class CmsKitPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(
            CmsKitPermissions.GroupName,
            L("Permission:CmsKit.Public")
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CmsKitResource>(name);
    }
}
