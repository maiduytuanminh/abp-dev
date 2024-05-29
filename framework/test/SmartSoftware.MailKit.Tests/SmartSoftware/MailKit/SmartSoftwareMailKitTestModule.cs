using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.MailKit;

[DependsOn(
    typeof(SmartSoftwareMailKitModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareTestBaseModule))]
public class SmartSoftwareMailKitTestModule : SmartSoftwareModule
{
}
