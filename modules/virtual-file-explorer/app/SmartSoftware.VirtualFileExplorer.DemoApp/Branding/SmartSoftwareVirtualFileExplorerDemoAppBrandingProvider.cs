using SmartSoftware.Ui.Branding;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.VirtualFileExplorer.DemoApp.Branding;

[Dependency(ReplaceServices = true)]
public class SmartSoftwareVirtualFileExplorerDemoAppBrandingProvider : DefaultBrandingProvider
{
    public SmartSoftwareVirtualFileExplorerDemoAppBrandingProvider()
    {
        AppName = "Virtual file explorer demo app";


    }

    public override string AppName { get; }

    public override string LogoUrl { get; }
}
