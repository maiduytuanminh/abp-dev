using SmartSoftware.Application;
using SmartSoftware.Modularity;

namespace SmartSoftware.PermissionManagement;

[DependsOn(
    typeof(SmartSoftwarePermissionManagementDomainModule),
    typeof(SmartSoftwarePermissionManagementApplicationContractsModule),
    typeof(SmartSoftwareDddApplicationModule)
    )]
public class SmartSoftwarePermissionManagementApplicationModule : SmartSoftwareModule
{

}
