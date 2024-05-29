using SmartSoftware.Modularity;

namespace SmartSoftware.FeatureManagement;

[DependsOn(
    typeof(SmartSoftwareFeatureManagementApplicationModule),
    typeof(SmartSoftwareFeatureManagementDomainTestModule)
    )]
public class FeatureManagementApplicationTestModule : SmartSoftwareModule
{

}
