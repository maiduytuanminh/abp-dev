using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.IdentityServer;

namespace SmartSoftware.IdentityServer;

[DependsOn(
    typeof(SmartSoftwareIdentityServerTestEntityFrameworkCoreModule),
    typeof(SmartSoftwarePermissionManagementDomainIdentityServerModule)
)]
public class SmartSoftwareIdentityServerDomainTestModule : SmartSoftwareModule
{

}
