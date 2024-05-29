using SmartSoftware.Authorization.Permissions;

namespace SmartSoftware.OpenIddict;

public class TestPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var testGroup = context.AddGroup(TestPermissionNames.Groups.TestGroup);
        testGroup.AddPermission(TestPermissionNames.MyPermission1);
        testGroup.AddPermission(TestPermissionNames.MyPermission2);
    }
}
