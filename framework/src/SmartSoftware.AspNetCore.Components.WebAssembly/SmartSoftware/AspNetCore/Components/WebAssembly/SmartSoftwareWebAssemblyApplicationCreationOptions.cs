using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace SmartSoftware.AspNetCore.Components.WebAssembly;

public class SmartSoftwareWebAssemblyApplicationCreationOptions
{
    public WebAssemblyHostBuilder HostBuilder { get; }

    public SmartSoftwareApplicationCreationOptions ApplicationCreationOptions { get; }

    public SmartSoftwareWebAssemblyApplicationCreationOptions(
        WebAssemblyHostBuilder hostBuilder,
        SmartSoftwareApplicationCreationOptions applicationCreationOptions)
    {
        HostBuilder = hostBuilder;
        ApplicationCreationOptions = applicationCreationOptions;
    }
}
