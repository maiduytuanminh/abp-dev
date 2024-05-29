using SmartSoftware.Authorization.Permissions;

namespace SmartSoftware.IdentityServer;

public class TestTestPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var testGroup = context.AddGroup(TestPermissionNames.Groups.TestGroup);
        testGroup.AddPermission(TestPermissionNames.MyPermission1);
        testGroup.AddPermission(TestPermissionNames.MyPermission2);
    }
}
