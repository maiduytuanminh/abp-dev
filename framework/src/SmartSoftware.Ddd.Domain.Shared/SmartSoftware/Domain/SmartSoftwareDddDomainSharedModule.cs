using SmartSoftware.EventBus.Abstractions;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.Domain;

[DependsOn(
    typeof(SmartSoftwareMultiTenancyAbstractionsModule),
    typeof(SmartSoftwareEventBusAbstractionsModule)
)]
public class SmartSoftwareDddDomainSharedModule : SmartSoftwareModule
{

}
