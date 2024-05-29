using SmartSoftware.Modularity;
using SmartSoftware.TenantManagement.EntityFrameworkCore;

namespace SmartSoftware.TenantManagement;

[DependsOn(
    typeof(SmartSoftwareTenantManagementEntityFrameworkCoreTestModule),
    typeof(SmartSoftwareTenantManagementTestBaseModule))]
public class SmartSoftwareSettingManagementDomainTestModule : SmartSoftwareModule
{

}
