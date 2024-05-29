using SmartSoftware.Authorization.Permissions;
using SmartSoftware.FeatureManagement.Localization;
using SmartSoftware.Localization;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.FeatureManagement;

public class FeaturePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var featureManagementGroup = context.AddGroup(
            FeatureManagementPermissions.GroupName,
            L("Permission:FeatureManagement"));

        featureManagementGroup.AddPermission(
            FeatureManagementPermissions.ManageHostFeatures,
            L("Permission:FeatureManagement.ManageHostFeatures"),
            multiTenancySide: MultiTenancySides.Host);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SmartSoftwareFeatureManagementResource>(name);
    }
}
