using SmartSoftware.Autofac;
using SmartSoftware.Modularity;
using SmartSoftware.SecurityLog;

namespace SmartSoftware.Security;

[DependsOn(
    typeof(SmartSoftwareSecurityModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareAutofacModule)
    )]
public class SmartSoftwareSecurityTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareSecurityLogOptions>(x =>
        {
            x.ApplicationName = "SmartSoftwareSecurityTest";
        });
    }
}
