using SmartSoftware.OpenIddict.EntityFrameworkCore;
using SmartSoftware.Modularity;
using SmartSoftware.PermissionManagement.OpenIddict;

namespace SmartSoftware.OpenIddict;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(OpenIddictEntityFrameworkCoreTestModule),
    typeof(SmartSoftwarePermissionManagementDomainOpenIddictModule)
    )]
public class OpenIddictDomainTestModule : SmartSoftwareModule
{

}
