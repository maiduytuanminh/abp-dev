using SmartSoftware.Application;
using SmartSoftware.Authorization;
using SmartSoftware.Modularity;

namespace SmartSoftware.SettingManagement;

[DependsOn(
    typeof(SmartSoftwareSettingManagementDomainSharedModule),
    typeof(SmartSoftwareDddApplicationContractsModule),
    typeof(SmartSoftwareAuthorizationAbstractionsModule)
)]
public class SmartSoftwareSettingManagementApplicationContractsModule : SmartSoftwareModule
{
}


