using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.Authorization;

[DependsOn(
    typeof(SmartSoftwareMultiTenancyAbstractionsModule)
)]
public class SmartSoftwareAuthorizationAbstractionsModule : SmartSoftwareModule
{

}
