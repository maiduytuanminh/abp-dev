using SmartSoftware.Http.Client.IdentityModel;
using SmartSoftware.Modularity;

namespace SmartSoftware.ClientSimulation;

[DependsOn(
    typeof(SmartSoftwareHttpClientIdentityModelModule)
    )]
public class ClientSimulationModule : SmartSoftwareModule
{

}
