using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn(typeof(SmartSoftwareAutofacModule))]
public class MyProjectNameModule : SmartSoftwareModule
{
}
