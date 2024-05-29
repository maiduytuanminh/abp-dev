using SmartSoftware.Modularity;

namespace SmartSoftware.Identity;

[DependsOn(
    typeof(SmartSoftwareIdentityApplicationModule),
    typeof(SmartSoftwareIdentityDomainTestModule)
    )]
public class SmartSoftwareIdentityApplicationTestModule : SmartSoftwareModule
{

}
