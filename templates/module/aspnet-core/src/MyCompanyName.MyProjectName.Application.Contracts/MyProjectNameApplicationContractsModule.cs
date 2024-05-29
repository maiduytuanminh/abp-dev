using SmartSoftware.Application;
using SmartSoftware.Modularity;
using SmartSoftware.Authorization;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    typeof(MyProjectNameDomainSharedModule),
    typeof(SmartSoftwareDddApplicationContractsModule),
    typeof(SmartSoftwareAuthorizationModule)
    )]
public class MyProjectNameApplicationContractsModule : SmartSoftwareModule
{

}
