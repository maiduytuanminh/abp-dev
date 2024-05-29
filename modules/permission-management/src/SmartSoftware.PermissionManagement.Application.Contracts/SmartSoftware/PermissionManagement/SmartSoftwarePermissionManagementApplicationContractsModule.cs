using SmartSoftware.Application;
using SmartSoftware.Authorization;
using SmartSoftware.Modularity;

namespace SmartSoftware.PermissionManagement;

[DependsOn(typeof(SmartSoftwareDddApplicationContractsModule))]
[DependsOn(typeof(SmartSoftwarePermissionManagementDomainSharedModule))]
[DependsOn(typeof(SmartSoftwareAuthorizationAbstractionsModule))]
public class SmartSoftwarePermissionManagementApplicationContractsModule : SmartSoftwareModule
{

}
