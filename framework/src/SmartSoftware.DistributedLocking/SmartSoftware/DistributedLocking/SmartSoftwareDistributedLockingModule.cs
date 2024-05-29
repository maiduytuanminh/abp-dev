using SmartSoftware.Modularity;
using SmartSoftware.Threading;

namespace SmartSoftware.DistributedLocking;

[DependsOn(
    typeof(SmartSoftwareDistributedLockingAbstractionsModule),
    typeof(SmartSoftwareThreadingModule)
    )]
public class SmartSoftwareDistributedLockingModule : SmartSoftwareModule
{
}
