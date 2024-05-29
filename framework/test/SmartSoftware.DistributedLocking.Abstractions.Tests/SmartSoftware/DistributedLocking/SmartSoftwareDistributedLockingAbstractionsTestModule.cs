using SmartSoftware.Autofac;
using SmartSoftware.Modularity;

namespace SmartSoftware.DistributedLocking;

[DependsOn(
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareDistributedLockingAbstractionsModule),
    typeof(SmartSoftwareAutofacModule)
)]
public class SmartSoftwareDistributedLockingAbstractionsTestModule : SmartSoftwareModule
{

}
