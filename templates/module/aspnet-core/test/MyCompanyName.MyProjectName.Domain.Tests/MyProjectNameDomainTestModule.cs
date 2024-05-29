using SmartSoftware.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    typeof(MyProjectNameDomainModule),
    typeof(MyProjectNameTestBaseModule)
)]
public class MyProjectNameDomainTestModule : SmartSoftwareModule
{

}
