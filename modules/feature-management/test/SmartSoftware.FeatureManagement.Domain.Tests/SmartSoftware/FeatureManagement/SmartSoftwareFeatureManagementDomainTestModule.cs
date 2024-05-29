using SmartSoftware.FeatureManagement.EntityFrameworkCore;
using SmartSoftware.Modularity;

namespace SmartSoftware.FeatureManagement;

[DependsOn(
    typeof(SmartSoftwareFeatureManagementEntityFrameworkCoreTestModule)
    )]
public class SmartSoftwareFeatureManagementDomainTestModule : SmartSoftwareModule
{

}
