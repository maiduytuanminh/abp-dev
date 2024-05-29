using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.RemoteServices;

namespace SmartSoftware.Http.Client;

[DependsOn(
    typeof(SmartSoftwareMultiTenancyModule),
    typeof(SmartSoftwareRemoteServicesModule)
)]
public class SmartSoftwareRemoteServicesTestModule: SmartSoftwareModule
{
}