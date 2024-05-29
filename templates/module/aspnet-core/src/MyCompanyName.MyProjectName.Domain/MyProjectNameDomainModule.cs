using SmartSoftware.Domain;
using SmartSoftware.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    typeof(SmartSoftwareDddDomainModule),
    typeof(MyProjectNameDomainSharedModule)
)]
public class MyProjectNameDomainModule : SmartSoftwareModule
{

}
