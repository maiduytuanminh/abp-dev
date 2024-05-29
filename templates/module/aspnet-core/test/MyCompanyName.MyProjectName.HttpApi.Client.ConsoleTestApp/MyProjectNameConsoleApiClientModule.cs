using SmartSoftware.Autofac;
using SmartSoftware.Http.Client.IdentityModel;
using SmartSoftware.Modularity;

namespace MyCompanyName.MyProjectName;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(MyProjectNameHttpApiClientModule),
    typeof(SmartSoftwareHttpClientIdentityModelModule)
    )]
public class MyProjectNameConsoleApiClientModule : SmartSoftwareModule
{

}
