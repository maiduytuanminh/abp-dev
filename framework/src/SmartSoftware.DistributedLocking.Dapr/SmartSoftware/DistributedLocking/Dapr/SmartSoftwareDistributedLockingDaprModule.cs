using SmartSoftware.Dapr;
using SmartSoftware.Modularity;

namespace SmartSoftware.DistributedLocking.Dapr;

[DependsOn(
    typeof(SmartSoftwareDistributedLockingAbstractionsModule),
    typeof(SmartSoftwareDaprModule))]
public class SmartSoftwareDistributedLockingDaprModule : SmartSoftwareModule
{
}