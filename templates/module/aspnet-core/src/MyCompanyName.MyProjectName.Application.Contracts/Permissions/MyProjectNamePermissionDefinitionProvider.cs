using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Localization;

namespace MyCompanyName.MyProjectName.Permissions;

public class MyProjectNamePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MyProjectNamePermissions.GroupName, L("Permission:MyProjectName"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MyProjectNameResource>(name);
    }
}
