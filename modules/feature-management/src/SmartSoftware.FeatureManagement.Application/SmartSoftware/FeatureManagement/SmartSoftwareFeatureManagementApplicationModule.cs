using SmartSoftware.Application;
using SmartSoftware.Modularity;

namespace SmartSoftware.FeatureManagement;

[DependsOn(
    typeof(SmartSoftwareFeatureManagementDomainModule),
    typeof(SmartSoftwareFeatureManagementApplicationContractsModule),
    typeof(SmartSoftwareDddApplicationModule)
    )]
public class SmartSoftwareFeatureManagementApplicationModule : SmartSoftwareModule
{

}
