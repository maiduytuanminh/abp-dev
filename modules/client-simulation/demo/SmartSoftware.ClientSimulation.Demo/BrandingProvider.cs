using SmartSoftware.Ui.Branding;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.ClientSimulation.Demo;

[Dependency(ReplaceServices = true)]
public class BrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Client Simulation";
}
